using KoiFarmShop.Business.Business.CustomerBusiness;
using KoiFarmShop.Business.Business.PromotionBusiness;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        //private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //public KoisController(FA_SE1854_SWP391_G3_KoiFarmShopContext context)
        //{
        //    _context = context;
        //}


        #region Get all list promotion 
        /// <summary>
        /// Get list of promotion 
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var c = await _customerService.GetAllCustomersAsync();
            return Ok(c);
        }
        #endregion

        #region Get list promotion filter
        /// <summary>
        /// Get list of promotions by filter
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet("/filterByCustomerIdAndUserId")]
        public async Task<IActionResult> GetCustomerList(int? customerId, int? userId)
        {
            var result = await _customerService.GetCustomerList(customerId, userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion
    }
}
