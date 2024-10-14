using AutoMapper;
using Azure.Core;
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
    public class ConsignmentRequestBusiness
    {
        private UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConsignmentRequestBusiness(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAllRequest()
        {
            try
            {
                var requests = await _unitOfWork.ConsignmentRequestRepository.GetAllAsync();
                var r = _mapper.Map<List<ConsignmentRequestDto>>(requests);
                if (requests == null)
                {
                    return new BusinessResult(404, "No request found.");
                }
                return new BusinessResult(200, "Successfully retrieved all requests", r);
            }
            catch (Exception ex)
            { 
                return new BusinessResult(500, $"Failed to retrieve requests: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> GetAllConsignmentDetail(int consignmentId)
        {
            try
            {
                var consignmentDetails = await _unitOfWork.ConsignmentDetailRepository.GetAllAsync(item => item.ConsignmentId == consignmentId);

                if (consignmentDetails == null || !consignmentDetails.Any())
                {
                    return new BusinessResult(404, "No details found for the specified request.");
                }

                var consignmentDetailDtos = _mapper.Map<List<ConsignmentDetailDto>>(consignmentDetails);

                return new BusinessResult(200, "Successfully retrieved all details for the specified consigntment.", consignmentDetailDtos);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve order items: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> GetRequestById(int id)
        {
            try
            {
                var requests = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(id);
                var r = _mapper.Map<List<ConsignmentRequestDto>>(requests);
                if (requests == null)
                {
                    return new BusinessResult(404, "No request found.");
                }
                return new BusinessResult(200, "Successfully retrieved all requests", r);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve requests: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> CreateConsignmentRequest(ConsignmentRequest request)
        {
            await _unitOfWork.BeginRequestAsync();
            try
            {
                _unitOfWork.ConsignmentRequestRepository.Create(request);
                await _unitOfWork.CommitRequestAsync();

                return new BusinessResult(200, "Request created successfully.", request);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackRequestAsync();
                return new BusinessResult(500, $"Failed to create consignment request: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> CreateConsignmentDetail(ConsignmentDetail detail)
        {
            await _unitOfWork.BeginRequestAsync();
            try
            {
                _unitOfWork.ConsignmentDetailRepository.Create(detail);
                await _unitOfWork.CommitRequestAsync();

                return new BusinessResult(200, "Detail created successfully.", detail);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackRequestAsync();
                return new BusinessResult(500, $"Failed to create consignment detail: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> UpdateRequest(ConsignmentRequestDto requestDto)
        {
            await _unitOfWork.BeginRequestAsync();
            try
            {
                var existingRequest = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(requestDto.ConsignmentId);

                if(existingRequest == null)
                {
                    return new BusinessResult(404, $"Request with ID {requestDto.ConsignmentId} not found.");
                }
                //need update
                existingRequest.SubAmount = existingRequest.ConsignmentDetails.Sum(detail => detail.SoldPrice);

                existingRequest.Vat = requestDto.Vat;
                requestDto.VatAmount = existingRequest.SubAmount * existingRequest.Vat;

                existingRequest.PromotionAmount = requestDto.PromotionAmount;
                requestDto.TotalAmount = existingRequest.SubAmount + existingRequest.VatAmount - existingRequest.PromotionAmount;
                existingRequest.TotalAmount = requestDto.TotalAmount;
                existingRequest.PaymentMethod = requestDto.PaymentMethod;
                existingRequest.PaymentStatus = requestDto.PaymentStatus;
                existingRequest.IsActive = requestDto.IsActive;
                existingRequest.Note = requestDto.Note;
                existingRequest.Status = requestDto.Status;
                existingRequest.IsOnline = requestDto.IsOnline;

                _unitOfWork.ConsignmentRequestRepository.Update(existingRequest);
                await _unitOfWork.CommitRequestAsync();

                return new BusinessResult(200, $"Request with ID {requestDto.ConsignmentId} updated successfully.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackOrderAsync();
                return new BusinessResult(500, $"Failed to update consignment request with ID {requestDto.ConsignmentId}: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> UpdateDetail(ConsignmentDetailDto detailDto)
        {
            await _unitOfWork.BeginRequestAsync();
            try
            {
                var existingDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(detailDto.ConsignmentDetailId);

                if (existingDetail == null)
                {
                    return new BusinessResult(404, $"Request with ID {detailDto.ConsignmentDetailId} not found.");
                }
                
                existingDetail.ConsignmentType = detailDto.ConsignmentType;
                existingDetail.MonthlyConsignmentFee = detailDto.MonthlyConsignmentFee;
                existingDetail.SoldPrice = detailDto.SoldPrice;
                existingDetail.HealthDescription = detailDto.HealthDescription;
                existingDetail.Weight = detailDto.Weight;
                existingDetail.Status = detailDto.Status;
                existingDetail.IsActive = detailDto.IsActive;
                existingDetail.Note = detailDto.Note;
                existingDetail.UpdatedAt = DateTime.Now;
                //change to get session from user?
                existingDetail.UpdatedBy = detailDto.UpdatedBy;

                _unitOfWork.ConsignmentDetailRepository.Update(existingDetail);
                await _unitOfWork.CommitRequestAsync();

                return new BusinessResult(200, $"Request with ID {detailDto.ConsignmentDetailId} updated successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackRequestAsync();
                return new BusinessResult(500, $"Failed to update consignment request with ID {detailDto.ConsignmentDetailId}: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> RemoveRequest(int requestId)
        {
            await _unitOfWork.BeginRequestAsync();
            try
            {
                var existingRequest = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(requestId);
                if (existingRequest == null)
                {
                    return new BusinessResult(404, $"Order item with ID {requestId} not found.");
                }

                _unitOfWork.ConsignmentRequestRepository.Remove(existingRequest);
                await _unitOfWork.CommitRequestAsync();

                return new BusinessResult(200, $"Order item with ID {requestId} deleted successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackRequestAsync();
                return new BusinessResult(500, $"Failed to delete order item with ID {requestId}: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> RemoveDetail (int detailId)
        {
            await _unitOfWork.BeginRequestAsync();
            try
            {
                var existingDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(detailId);
                if (existingDetail == null)
                {
                    return new BusinessResult(404, $"Order item with ID {detailId} not found.");
                }

                _unitOfWork.ConsignmentDetailRepository.Remove(existingDetail);
                await _unitOfWork.CommitRequestAsync();

                return new BusinessResult(200, $"Order item with ID {detailId} deleted successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackRequestAsync();
                return new BusinessResult(500, $"Failed to delete order item with ID {detailId}: {ex.Message}");
            }
        }
    }
}
