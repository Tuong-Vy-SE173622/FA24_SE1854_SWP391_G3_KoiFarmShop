using KoiFarmShop.Business.Dto;

namespace KoiFarmShop.Business.Business.KoiTypeBusiness
{
    public interface IKoiTypeService
    {
        Task<IEnumerable<KoiTypeDto>> GetAllKoiTypesAsync();
        Task<KoiTypeDto> GetKoiTypeByIdAsync(int id);
        Task<int> CreateKoiTypeAsync(KoiTypeCreateDto koiTypeDto);
        Task<int> UpdateKoiTypeAsync(KoiTypeUpdateDto koiTypeDto);
        Task<bool> RemoveKoiTypeAsync(int id);
    }
}
