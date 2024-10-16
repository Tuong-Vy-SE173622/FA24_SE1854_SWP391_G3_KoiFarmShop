using KoiFarmShop.Business.Business.PromotionBusiness;
using KoiFarmShop.Business.Business.UserBusiness;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        //private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        //public KoisController(FA_SE1854_SWP391_G3_KoiFarmShopContext context)
        //{
        //    _context = context;
        //}


        #region Get list promotion filter
        /// <summary>
        /// Get list of users by filter
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        public async Task<IActionResult> GetPromotions(int? promotionId)
        {
            //return await _context.Kois.ToListAsync();
            var result = await _promotionService.GetPromotionList(promotionId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion
    }
}
