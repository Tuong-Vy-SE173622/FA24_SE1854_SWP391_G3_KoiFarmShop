using KoiFarmShop.Business.Dto.Consigments;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.ConsignmentBusiness
{
    public interface IConsignmentDetailService
    {
        Task<ConsignmentDetailResponseDto> CreateConsignmentDetailAsync(ConsignmentDetailCreateDto consignmentDetailCreateDto);
        Task<ConsignmentDetailResponseDto> UpdateConsignmentDetailAsync(int id, ConsignmentDetailUpdateDto consignmentDetailUpdateDto);
        Task DeleteConsignmentDetailAsync(int id);
        Task<IEnumerable<ConsignmentDetailResponseDto>> GetDetailsByConsignmentRequestIdAsync(int consignmentRequestId);
    }

}
