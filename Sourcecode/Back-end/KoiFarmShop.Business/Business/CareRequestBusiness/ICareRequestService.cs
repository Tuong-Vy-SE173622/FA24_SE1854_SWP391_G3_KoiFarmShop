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
        Task<IEnumerable<CareRequestResponseDto>> GetAllCareRequestAsync();
        Task<CareRequestResponseDto> GetCareRequestByIdAsync(int id);
        Task<CareRequestResponseDto> CreateCareRequestAsync(CareRequestCreateDto createDto, string? currentUser);
        Task<CareRequestResponseDto> UpdateCareRequestAsync(int id, CareRequestUpdateDto updateDto);
        Task<bool> DeleteCareRequestAsync(int id);
    }
}
