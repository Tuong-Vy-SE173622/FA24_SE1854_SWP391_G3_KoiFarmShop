using KoiFarmShop.Business.Dto.CareRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.CareRequestBusiness
{
    public interface ICareRequestService
    {
        //Task<IEnumerable<CareRequestResponseDto>> GetAllCareRequestAsync();
        Task<PaginatedResult<CareRequestDto>> GetAllCareRequestAsync(CareRequestFilterDto filterDto);
        Task<CareRequestResponseDto> GetCareRequestByIdAsync(int id);
        Task<CareRequestResponseDto> GetCareRequestByCustomerIdAsync(int customerId);
        Task<CareRequestResponseDto> CreateCareRequestAsync(CareRequestCreateDto createDto, string? currentUser);
        Task<CareRequestResponseDto> UpdateCareRequestAsync(int id, CareRequestUpdateDto updateDto, string? currentUser);
        Task<CareRequestResponseDto> ApproveCareRequestAsync(int careRequestId, string? currentUser);
        Task<CareRequestResponseDto> RejectCareRequestAsync(int careRequestId, string? currentUser);
        Task<bool> DeleteCareRequestAsync(int id);
        Task<CareRequestDto> UpdateCareRequestStatusAfterPaymentAsync(int careRequestId);
    }
}
