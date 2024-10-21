using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Promotion;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.PromotionBusiness
{
    public interface IPromotionService
    {
        public Task<IEnumerable<PromotionDto>> GetAllPromotionsAsync();
        public Task<ResultDto> GetPromotionList(int? promotionId);
        //public Task<int> CreatePromotionAsync(PromotionCreateDto promotionCreateDto);
        public Task<ResultDto> AddNewPromotion(PromotionCreateDto model, ClaimsPrincipal userCreate);
        public Task<ResultDto> UpdatePromotion(int promotionId, PromotionCreateDto promotionDto, ClaimsPrincipal userUpdate);
        public Task<ResultDto> DeletePromotion(DeletePromotionDto request, ClaimsPrincipal userDelete);
    }
}
