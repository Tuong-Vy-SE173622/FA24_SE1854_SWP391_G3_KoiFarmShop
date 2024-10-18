﻿using KoiFarmShop.Business.Business.PromotionBusiness;
using KoiFarmShop.Business.Business.UserBusiness;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data.Models;
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

        #region Get all list promotion 
        /// <summary>
        /// Get list of users by filter
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            var p = await _promotionService.GetAllPromotionsAsync();
            return Ok(p);
        }
        #endregion


        #region Get list promotion filter
        /// <summary>
        /// Get list of users by filter
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotionList(int? promotionId)
        {
            var result = await _promotionService.GetPromotionList(promotionId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion


        #region Add a new voucher
        /// <summary>
        /// Add a new user by admin
        /// </summary>
        /// <returns>Status of action</returns>
        [HttpPost]
        public async Task<ActionResult> AddNewPromotion(PromotionCreateDto model)
        {
            try
            {
                var currentUser = HttpContext.User;
                var promotionResult = await _promotionService.AddNewPromotion(model, currentUser);
                var result = new ResultDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Data = promotionResult
                };
                return Ok(result); // Return 200 OK with the registration response
            }
            catch (Exception ex)
            {
                var result = new ResultDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = ex.Message
                }; // Return 400 Bad Request with the error message
                return StatusCode(500, result);
            }
        }
        #endregion
    }
}
