using KoiFarmShop.Business.Dto.Consigments;

namespace KoiFarmShop.Business.Business.ConsignmentBusiness
{
    public interface IConsignmentRequestService
    {
        Task<ConsignmentRequestResponseDto> CreateConsignmentRequestAsync(ConsignmentRequestCreateDto consignmentRequestCreateDto);
        Task<ConsignmentRequestResponseDto> UpdateConsignmentRequestAsync(int id, ConsignmentRequestUpdateDto consignmentRequestUpdateDto);
        Task DeleteConsignmentRequestAsync(int id);
        Task<ConsignmentRequestResponseDto> GetConsignmentRequestByIdAsync(int id);
        Task<IEnumerable<ConsignmentRequestResponseDto>> GetAllConsignmentRequestsAsync();
    }

}
