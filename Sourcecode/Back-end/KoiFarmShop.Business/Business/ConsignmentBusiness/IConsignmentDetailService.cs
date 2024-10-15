using KoiFarmShop.Business.Dto.Consigments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.ConsignmentBusiness
{
    public interface IConsignmentDetailService
    {
        Task<ConsignmentDetailResponseDto> CreateConsignmentDetailAsync(ConsignmentDetailCreateDto createDto);
        Task<ConsignmentDetailResponseDto> UpdateConsignmentDetailAsync(int id, ConsignmentDetailUpdateDto updateDto);
        Task<bool> DeleteConsignmentDetailAsync(int id);
        Task<ConsignmentDetailResponseDto> GetConsignmentDetailByIdAsync(int id);
        Task<IEnumerable<ConsignmentDetailResponseDto>> GetAllConsignmentDetailsAsync();
    }

}
