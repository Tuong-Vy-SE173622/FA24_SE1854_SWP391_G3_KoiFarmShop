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
        Task<ResultDto> CreateKoiTypeAsync(KoiTypeCreateDto koiTypeCreateDto, ClaimsPrincipal userCreate);
        Task<ResultDto> UpdateKoiTypeAsync(int koiTypeId, KoiTypeUpdateDto koiTypeUpdateDto, ClaimsPrincipal userUpdate);
        Task<bool> RemoveKoiTypeAsync(int id);
    }
}
