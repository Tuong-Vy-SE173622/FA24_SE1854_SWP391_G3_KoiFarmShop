using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business
{
    public class CareRequestBusiness
    {
        private UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CareRequestBusiness(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAllCareRequest()
        {
            try
            {
                var requests = await _unitOfWork.CareRequestRepository.GetAllAsync();
                var r = _mapper.Map<List<CareRequestDto>>(requests);
                if (requests == null)
                {
                    return new BusinessResult(404, "No care request found.");
                }
                return new BusinessResult(200, "Successfully retrieved all care requests", r);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve care requests: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> GetAllCareRequestDetail(int careRequestId)
        {
            try
            {
                var careRequestDetails = await _unitOfWork.CareRequestDetailRepository.GetAllAsync(item => item.RequestId == careRequestId);

                if (careRequestDetails == null || !careRequestDetails.Any())
                {
                    return new BusinessResult(404, "No details found for the specified care request.");
                }

                var careRequestDetailDtos = _mapper.Map<List<CareRequestDetailDto>>(careRequestDetails);

                return new BusinessResult(200, "Successfully retrieved all details for the specified care request.", careRequestDetailDtos);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve care request details: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> GetCareRequestById(int id)
        {
            try
            {
                var requests = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
                var r = _mapper.Map<List<CareRequestDto>>(requests);
                if (requests == null)
                {
                    return new BusinessResult(404, "No care request found.");
                }
                return new BusinessResult(200, "Successfully retrieved all care requests", r);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve care requests: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> CreateCareRequest(CareRequest request)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.CareRequestRepository.Create(request);
                await _unitOfWork.CommitTransactionAsync();

                return new BusinessResult(200, "Care request created successfully.", request);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new BusinessResult(500, $"Failed to create care request: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> CreateCareRequestDetail(CareRequestDetail detail)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.CareRequestDetailRepository.Create(detail);
                await _unitOfWork.CommitTransactionAsync();

                return new BusinessResult(200, "Care request detail created successfully.", detail);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new BusinessResult(500, $"Failed to create care request detail: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> UpdateCareRequest(CareRequestDto careRequestDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingCareRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(careRequestDto.RequestId);

                if (existingCareRequest == null)
                {
                    return new BusinessResult(404, $"Care request with ID {careRequestDto.RequestId} not found.");
                }
                //need update
                existingCareRequest.Status = careRequestDto.Status;
                existingCareRequest.IsActive = careRequestDto.IsActive;
                existingCareRequest.Note = careRequestDto.Note;
                existingCareRequest.UpdatedAt = DateTime.Now;
                existingCareRequest.UpdatedBy = careRequestDto.UpdatedBy;

                _unitOfWork.CareRequestRepository.Update(existingCareRequest);
                await _unitOfWork.CommitTransactionAsync();

                return new BusinessResult(200, $"Care request with ID {careRequestDto.RequestId} updated successfully.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransactionAsync();
                return new BusinessResult(500, $"Failed to update care request with ID {careRequestDto.RequestId}: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> UpdateDetail(CareRequestDetailDto careDetailDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingCareDetail = await _unitOfWork.CareRequestDetailRepository.GetByIdAsync(careDetailDto.RequestDetailId);

                if (existingCareDetail == null)
                {
                    return new BusinessResult(404, $"Request with ID {careDetailDto.RequestDetailId} not found.");
                }
                existingCareDetail.CareMethod = careDetailDto.CareMethod;
                existingCareDetail.Status = careDetailDto.Status;
                existingCareDetail.Note = careDetailDto.Note;
                existingCareDetail.UpdatedAt = DateTime.Now;

                //change to get session from user?
                existingCareDetail.UpdatedBy = careDetailDto.UpdatedBy;

                _unitOfWork.CareRequestDetailRepository.Update(existingCareDetail);
                await _unitOfWork.CommitTransactionAsync();

                return new BusinessResult(200, $"Care request detail with ID {careDetailDto.RequestDetailId} updated successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new BusinessResult(500, $"Failed to update consignment request with ID {careDetailDto.RequestDetailId}: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> RemoveCareRequest(int careRequestId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(careRequestId);
                if (existingRequest == null)
                {
                    return new BusinessResult(404, $"Care request with ID {careRequestId} not found.");
                }

                _unitOfWork.CareRequestRepository.Remove(existingRequest);
                await _unitOfWork.CommitTransactionAsync();

                return new BusinessResult(200, $"Care request with ID {careRequestId} deleted successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new BusinessResult(500, $"Failed to delete care request with ID {careRequestId}: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> RemoveCareDetail(int detailId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingDetail = await _unitOfWork.CareRequestDetailRepository.GetByIdAsync(detailId);
                if (existingDetail == null)
                {
                    return new BusinessResult(404, $"Care request detail with ID {detailId} not found.");
                }

                _unitOfWork.CareRequestDetailRepository.Remove(existingDetail);
                await _unitOfWork.CommitTransactionAsync();

                return new BusinessResult(200, $"Care request detail with ID {detailId} deleted successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new BusinessResult(500, $"Failed to delete care request detail with ID {detailId}: {ex.Message}");
            }
        }
    }
}
