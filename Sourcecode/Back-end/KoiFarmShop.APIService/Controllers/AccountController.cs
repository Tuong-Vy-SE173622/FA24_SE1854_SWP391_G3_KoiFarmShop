using KoiFarmShop.Business.Business.AccountBusiness;
using KoiFarmShop.Business.Business.UserBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IUserService userService, IConfiguration configuration)
        {
            _accountService = accountService;
            _userService = userService;
            _configuration = configuration;
        }

        #region Add a new user
        /// <summary>
        /// Add a new user by admin
        /// </summary>
        /// <returns>Status of action</returns>
        [HttpPost]
        public async Task<ActionResult> AddNewUser(RegisterDto model)
        {
            try
            {
                var currentUser = HttpContext.User;
                var registerResult = await _accountService.AddNewUser(model, currentUser);
                var result = new ResultDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Data = registerResult
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

        #region Add a new customer
        /// <summary>
        /// Add a new customer associated with a user
        /// </summary>
        /// <returns>Status of action</returns>
        [HttpPost("/add-new-customer")]
        public async Task<ActionResult> AddNewCustomer(CustomerDto model)
        {
            try
            {
                var currentUser = HttpContext.User;
                var addCustomerResult = await _accountService.AddNewCustomer(model, currentUser);

                if (!addCustomerResult.IsSuccess)
                {
                    return BadRequest(addCustomerResult);
                }

                return Ok(new ResultDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Data = addCustomerResult
                });
            }
            catch (Exception ex)
            {
                var result = new ResultDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = ex.Message
                };
                return StatusCode(500, result);
            }
        }
        #endregion
    }
}