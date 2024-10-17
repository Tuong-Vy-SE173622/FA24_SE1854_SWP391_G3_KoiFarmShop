using KoiFarmShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.PromotionBusiness
{
    public interface IPromotionService
    {
        public Task<ResultDto> GetPromotionList(int? promotionId);
    }
}
