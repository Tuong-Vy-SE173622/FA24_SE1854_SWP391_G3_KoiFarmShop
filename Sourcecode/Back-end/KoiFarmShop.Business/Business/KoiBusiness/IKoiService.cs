using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public interface IKoiService
    {
        Task<IEnumerable<KoiDto>> GetAllKoisAsync();
        Task<PaginatedResult<KoiDto>> GetAllKoisAsync(KoiFilterDto filterDto);
        Task<KoiDto> GetKoiByIdAsync(int id);
        Task<int> CreateKoiAsync(KoiCreateDto koiDto);
        Task<int> UpdateKoiAsync(int id, KoiUpdateDto koiDto);
        Task<bool> RemoveKoiAsync(int id);
        Task<HashSet<string>> GetAllKoiOrigins();
        Task<ResultDto> CreateKoiWithImageAsync(int koiTypeId, List<KoiCreateWithImageDto> koiCreateDto, ClaimsPrincipal userCreate);
    }
}
