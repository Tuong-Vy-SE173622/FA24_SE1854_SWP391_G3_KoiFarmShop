using KoiFarmShop.Business.Dto.Consigments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.ConsignmentBusiness
{
    public interface IConsignmentRequestService
    {
        Task<ConsignmentRequestResponseDto> CreateConsignmentRequestAsync(ConsignmentRequestCreateDto createDto);
        Task<ConsignmentRequestResponseDto> UpdateConsignmentRequestAsync(int id, ConsignmentUpdateDto updateDto);
        Task<bool> DeleteConsignmentRequestAsync(int id);
        Task<ConsignmentRequestResponseDto> GetConsignmentRequestByIdAsync(int id);
        Task<IEnumerable<ConsignmentRequestResponseDto>> GetAllConsignmentRequestsAsync();
    }

}
