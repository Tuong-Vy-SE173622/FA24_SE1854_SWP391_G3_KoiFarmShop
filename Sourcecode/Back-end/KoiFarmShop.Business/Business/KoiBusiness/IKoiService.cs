using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public interface IKoiService
    {
        Task<PaginatedResult<KoiDto>> GetAllKoisForAdminAsync(KoiFilterDto filterDto);
        Task<PaginatedResult<KoiDto>> GetAllKoisAsync(KoiFilterDto filterDto);
        Task<KoiDto> GetKoiByIdAsync(int id);
        Task<int> CreateKoiAsync(KoiCreateDto koiDto, string? currentUser);
        Task<int> CreateKoiForCustomerAsync(KoiCreateForCustomerDto koiDto, string? currentUser);
        Task<int> UpdateKoiAsync(int id, KoiUpdateDto koiDto, string? currentUser);
        Task<bool> UpdateForListSoldKoisAsynce(ListSoldKois list);
        Task<bool> RemoveKoiAsync(int id);
        Task<HashSet<string>> GetAllKoiOrigins();
        Task<ResultDto> ApproveOrRejectKoiForCareRequest(KoiApproveRequest request, string? currentUser);
        Task<ResultDto> ApproveOrRejectKoiForConsignment(KoiApproveRequest request, string? currentUser);
        Task<ResultDto> GetAllKoisCreatedByUserId(int userId);
        Task<string> Test();

        Task<Koi?> GetKoiWithConsignment(int id);
    }
}
