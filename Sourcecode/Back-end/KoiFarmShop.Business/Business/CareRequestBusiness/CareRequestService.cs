using AutoMapper;
using KoiFarmShop.Business.Dto.CareRequests;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<CareRequestResponseDto>> GetAllCareRequestAsync()
        {
            var careRequests = await _unitOfWork.CareRequestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CareRequestResponseDto>>(careRequests);
        }

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

            careRequest.RequestId = _unitOfWork.CareRequestRepository.GetAll().OrderByDescending(cr => cr.RequestId).Select(cr => cr.RequestId).FirstOrDefault() + 1;
            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequest.CreatedBy = currentUser;
            careRequest.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CareRequestRepository.CreateAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CareRequestResponseDto>(careRequest);
        }

        public async Task<CareRequestResponseDto> UpdateCareRequestAsync(int id, CareRequestUpdateDto updateDto, string? currentUser)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
            if (careRequest == null) return null;

            updateDto.UpdatedAt = DateTime.UtcNow;
            _mapper.Map(updateDto, careRequest);
            careRequest.UpdatedAt = DateTime.UtcNow;
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
