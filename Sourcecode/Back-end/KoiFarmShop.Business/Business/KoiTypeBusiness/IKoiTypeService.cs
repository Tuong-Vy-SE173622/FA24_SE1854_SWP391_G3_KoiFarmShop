using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.KoiTypes;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.KoiTypeBusiness
{
    public interface IKoiTypeService
    {
        Task<IEnumerable<KoiTypeDto>> GetAllKoiTypesAsync();
        Task<PaginatedResult<KoiTypeDto>> GetAllKoiTypesAsync(KoiTypeFilterDto filterDto);
        Task<KoiTypeDto> GetKoiTypeByIdAsync(int id);
        Task<int> CreateKoiTypeAsync(KoiTypeCreateDto koiTypeDto, string? currentUser);
        Task<int> UpdateKoiTypeAsync(int id, KoiTypeUpdateDto koiTypeDto, string? currentUser);
        Task<bool> RemoveKoiTypeAsync(int id); 
    }
}
