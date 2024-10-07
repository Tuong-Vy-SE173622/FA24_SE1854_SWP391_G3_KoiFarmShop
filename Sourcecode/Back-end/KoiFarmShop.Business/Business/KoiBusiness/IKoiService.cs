using KoiFarmShop.Business.Dto;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public interface IKoiService
    {
        Task<IEnumerable<KoiDto>> GetAllKoisAsync();
        Task<KoiDto> GetKoiByIdAsync(int id);
        Task<int> CreateKoiAsync(KoiDto koiDto);
        Task<int> UpdateKoiAsync(KoiDto koiDto);
        Task<bool> RemoveKoiAsync(int id);
    }
}
