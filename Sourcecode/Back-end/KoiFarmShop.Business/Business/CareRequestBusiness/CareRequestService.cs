using AutoMapper;
using KoiFarmShop.Business.Dto.CareRequests;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequest;

namespace KoiFarmShop.Business.Business.CareRequestBusiness
{
    public class CareRequestService : ICareRequestService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CareRequestService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CareRequestDto>> GetAllCareRequestAsync (CareRequestFilterDto filterDto)
        {
            var query = _unitOfWork.CareRequestRepository.GetQueryable();
            if (filterDto.CustomerId.HasValue)
            {
                query = query.Where(cr => cr.CustomerId == filterDto.CustomerId);
            }

            if (!string.IsNullOrEmpty(filterDto.CarePlanName))
            {
                query = query.Where(cr => cr.CarePlanId == _unitOfWork.CarePlanRepository.GetAll().FirstOrDefault(cp => cp.Name == filterDto.CarePlanName).CarePlanId);
            }

            if (filterDto.Status.HasValue)
            {
                query = query.Where(cr => cr.Status == filterDto.Status);
            }
            if (filterDto.IsSortedByName)
                query = filterDto.IsAscending
                    ? query.OrderBy(cr => cr.CarePlan.Name)
                    : query.OrderByDescending(cr => cr.CarePlan.Name);

            var totalRecords = await query.CountAsync();

            var pagedCareRequests = await query
                .Skip((filterDto.PageNumber - 1) * filterDto.PageSize)
                .Take(filterDto.PageSize)
                .Select(cr => new CareRequestDto
                {
                    CareRequestId = cr.CareRequestId,
                    CustomerId = cr.CustomerId,
                    KoiId = cr.CustomerId,
                    CarePlanId = cr.CarePlanId,
                    StartDate = cr.StartDate,
                    Status = CareRequestStatus.PendingApproval,
                    TotalAmount = cr.TotalAmount,
                    CreatedBy = cr.CreatedBy,
                    UpdatedBy = cr.UpdatedBy,
                    CarePlan = cr.CarePlan,
                    CareRequestDetail = cr.CareRequestDetails
                })
                .ToListAsync();

            var careRequestDtos = _mapper.Map<List<CareRequestDto>>(pagedCareRequests);

            return new PaginatedResult<CareRequestDto>
            {
                Data = pagedCareRequests,
                TotalRecords = totalRecords,
                PageNumber = filterDto.PageNumber,
                PageSize = filterDto.PageSize
            };
        }

        public async Task<CareRequestResponseDto> GetCareRequestByIdAsync(int id)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
            return careRequest != null ? _mapper.Map<CareRequestResponseDto>(careRequest) : null;
        }

        public async Task<CareRequestResponseDto> GetCareRequestByCustomerIdAsync(int customerId)
        {
            var careRequest = _unitOfWork.CareRequestRepository.GetAll().Where(cr => cr.CustomerId == customerId);
            return careRequest != null ? _mapper.Map<CareRequestResponseDto>(careRequest) : null;
        }

        public async Task<CareRequestResponseDto> ApproveCareRequestAsync(int careRequestId, string? currentUser)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(careRequestId);
            if (careRequest == null)
            {
                throw new NotFoundException($"Care request doesn't exist.");
            }


            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequest.UpdatedBy = currentUser;
            careRequest.Status = CareRequestStatus.Active;
            await _unitOfWork.CareRequestRepository.UpdateAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CareRequestResponseDto>(careRequest);
        }

        public async Task<CareRequestResponseDto> RejectCareRequestAsync(int careRequestId, string? currentUser)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(careRequestId);
            if (careRequest == null)
            {
                throw new NotFoundException($"Care request doesn't exist.");
            }


            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequest.UpdatedBy = currentUser;
            careRequest.Status = CareRequestStatus.Rejected;
            await _unitOfWork.CareRequestRepository.UpdateAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CareRequestResponseDto>(careRequest);
        }

        //public async Task<IEnumerable<CareRequestResponseDto>> GetAllCareRequestAsync()
        //{
        //    var careRequests = await _unitOfWork.CareRequestRepository.GetAllAsync();
        //    return _mapper.Map<IEnumerable<CareRequestResponseDto>>(careRequests);
        //}

        public async Task<CareRequestResponseDto> CreateCareRequestAsync(CareRequestCreateDto createDto, string? currentUser)
        {
            var user = _unitOfWork.UserRepository.GetById((int)createDto.CustomerId);
            if (user == null)
            {
                throw new NotFoundException("Customer Id does not exist");
            }

            var koi = await _unitOfWork.KoiRepository.GetByIdAsync((int)createDto.KoiId);
            if (koi == null)
                throw new NotFoundException("Koi not found");

            var careRequest = _mapper.Map<CareRequest>(createDto);

            careRequest.CareRequestId = _unitOfWork.CareRequestRepository.GetAll().OrderByDescending(cr => cr.CareRequestId).Select(cr => cr.CareRequestId).FirstOrDefault() + 1;
            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequest.CreatedBy = currentUser;
            //careRequest.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CareRequestRepository.CreateAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CareRequestResponseDto>(careRequest);
        }

        public async Task<CareRequestResponseDto> UpdateCareRequestAsync(int id, CareRequestUpdateDto updateDto, string? currentUser)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
            if (careRequest == null) { 
                throw new NotFoundException($"Care request doesn't exist.");
            }

            
            _mapper.Map(updateDto, careRequest);
            //careRequest.UpdatedAt = DateTime.UtcNow;
            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequest.UpdatedBy = currentUser;
            await _unitOfWork.CareRequestRepository.UpdateAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CareRequestResponseDto>(careRequest);
        }

        public async Task<bool> DeleteCareRequestAsync(int id)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
            if (careRequest == null) return false;

            await _unitOfWork.CareRequestRepository.RemoveAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
