using AutoMapper;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;

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

        public async Task<ConsignmentRequestResponseDto> CreateConsignmentRequestAsync(ConsignmentRequestCreateDto consignmentRequestCreateDto)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(consignmentRequestCreateDto.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException("Customer Id does not exist");
            }

            var consignmentRequest = _mapper.Map<ConsignmentRequest>(consignmentRequestCreateDto);
            await _unitOfWork.ConsignmentRequestRepository.CreateAsync(consignmentRequest);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ConsignmentRequestResponseDto>(consignmentRequest);
        }

        public async Task<ConsignmentRequestResponseDto> UpdateConsignmentRequestAsync(int id, ConsignmentRequestUpdateDto consignmentRequestUpdateDto)
        {
            var consignmentRequest = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(id);
            if (consignmentRequest == null) throw new KeyNotFoundException("Consignment Request not found");

            //need to check customer id
            _mapper.Map(consignmentRequestUpdateDto, consignmentRequest);
            await _unitOfWork.ConsignmentRequestRepository.UpdateAsync(consignmentRequest);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ConsignmentRequestResponseDto>(consignmentRequest);
        }

        public async Task DeleteConsignmentRequestAsync(int id)
        {
            var consignmentRequest = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(id);
            if (consignmentRequest == null) throw new KeyNotFoundException("Consignment Request not found");

            //if(await _unitOfWork.ConsignmentRequestRepository.HasAnyAssociatedDetails(id))
            //{
            //    throw new Exception("Consignment details with this consignment request still exist");
            //}

            await _unitOfWork.ConsignmentRequestRepository.RemoveAsync(consignmentRequest);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ConsignmentRequestResponseDto> GetConsignmentRequestByIdAsync(int id)
        {
            var consignmentRequest = await _unitOfWork.ConsignmentRequestRepository.GetRequestWithDetailsAsync(id);
            return _mapper.Map<ConsignmentRequestResponseDto>(consignmentRequest);
        }

        public async Task<IEnumerable<ConsignmentRequestResponseDto>> GetAllConsignmentRequestsAsync()
        {
            var consignmentRequests = await _unitOfWork.ConsignmentRequestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConsignmentRequestResponseDto>>(consignmentRequests);
        }
    }


}
