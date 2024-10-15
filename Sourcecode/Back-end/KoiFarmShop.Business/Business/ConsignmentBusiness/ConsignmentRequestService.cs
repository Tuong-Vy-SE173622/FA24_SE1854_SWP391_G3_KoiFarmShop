using AutoMapper;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.ConsignmentBusiness
{
    public class ConsignmentRequestService : IConsignmentRequestService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConsignmentRequestService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ConsignmentRequestResponseDto> CreateConsignmentRequestAsync(ConsignmentRequestCreateDto createDto)
        {
            var consignmentRequest = _mapper.Map<ConsignmentRequest>(createDto);
            await _unitOfWork.ConsignmentRequestRepository.CreateAsync(consignmentRequest);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ConsignmentRequestResponseDto>(consignmentRequest);
        }

        public async Task<ConsignmentRequestResponseDto> UpdateConsignmentRequestAsync(int id, ConsignmentUpdateDto updateDto)
        {
            var consignmentRequest = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(id);
            if (consignmentRequest == null) return null;

            _mapper.Map(updateDto, consignmentRequest);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ConsignmentRequestResponseDto>(consignmentRequest);
        }

        public async Task<bool> DeleteConsignmentRequestAsync(int id)
        {
            var consignmentRequest = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(id);
            if (consignmentRequest == null) return false;

            await _unitOfWork.ConsignmentRequestRepository.RemoveAsync(consignmentRequest);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<ConsignmentRequestResponseDto> GetConsignmentRequestByIdAsync(int id)
        {
            var consignmentRequest = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(id);
            return consignmentRequest != null ? _mapper.Map<ConsignmentRequestResponseDto>(consignmentRequest) : null;
        }

        public async Task<IEnumerable<ConsignmentRequestResponseDto>> GetAllConsignmentRequestsAsync()
        {
            var consignmentRequests = await _unitOfWork.ConsignmentRequestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConsignmentRequestResponseDto>>(consignmentRequests);
        }
    }

}
