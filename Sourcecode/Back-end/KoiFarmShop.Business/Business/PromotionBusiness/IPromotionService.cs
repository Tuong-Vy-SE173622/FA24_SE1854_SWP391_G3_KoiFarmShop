using KoiFarmShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.PromotionBusiness
{
    public interface IPromotionService
    {
        public Task<IEnumerable<PromotionDto>> GetAllPromotionsAsync();
        public Task<PromotionDto> GetPromotionByIdAsync(int id);
        public Task<ResultDto> GetPromotionList(int? promotionId);
        //public Task<int> CreatePromotionAsync(PromotionCreateDto promotionCreateDto);
        public Task<ResultDto> AddNewPromotion(PromotionCreateDto model, ClaimsPrincipal userCreate);
        public Task<ResultDto> UpdatePromotion(int promotionId, PromotionCreateDto promotionDto, ClaimsPrincipal userUpdate);
    }
}
