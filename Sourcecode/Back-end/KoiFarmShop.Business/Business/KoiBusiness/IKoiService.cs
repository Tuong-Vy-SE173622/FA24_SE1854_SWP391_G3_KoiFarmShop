using KoiFarmShop.Business.Dto.Kois;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public interface IKoiService
    {
        Task<PaginatedResult<KoiDto>> GetAllKoisForAdminAsync(KoiFilterDto filterDto);
        Task<PaginatedResult<KoiDto>> GetAllKoisAsync(KoiFilterDto filterDto);
        Task<KoiDto> GetKoiByIdAsync(int id);
        Task<int> CreateKoiAsync(KoiCreateDto koiDto, string currentUser);
        Task<int> UpdateKoiAsync(int id, KoiUpdateDto koiDto, string currentUser);
        Task<bool> RemoveKoiAsync(int id);
        Task<HashSet<string>> GetAllKoiOrigins();
    }
}
