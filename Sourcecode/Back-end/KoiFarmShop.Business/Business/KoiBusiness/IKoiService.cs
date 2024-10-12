using KoiFarmShop.Business.Dto;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public interface IKoiService
    {
        Task<IEnumerable<KoiDto>> GetAllKoisAsync();
        Task<KoiDto> GetKoiByIdAsync(int id);
        Task<int> CreateKoiAsync(KoiCreateDto koiDto);
        Task<int> UpdateKoiAsync(KoiUpdateDto koiDto);
        Task<bool> RemoveKoiAsync(int id);
    }
}
