using KoiFarmShop.Business.Business.UserBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        #region Get list user filter
        /// <summary>
        /// Get list of users by filter
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers(int? userId, string? name, string? email, string? phone, string? firstName, string? lastName)
        {
            //return await _context.Kois.ToListAsync();
            var result = await _userService.GetUserList(userId, name, email, phone, firstName, lastName);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        #region Search user
        /// <summary>
        /// Search user by keyword
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet("{keyword}")]
        public async Task<ActionResult<ResultDto>> SearchUser(string keyword)
        {
            //var koi = await _context.Kois.FindAsync(id);
            var result = await _userService.SearchUserByKeyword(keyword);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion


        #region Update User
        /// <summary>
        /// Update a user
        /// </summary>
        /// <returns>Status of action</returns>
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(int userId, UserDto userDto)
        {
            var currentUser = HttpContext.User;
            var updateResult = await _userService.UpdateUser(userId, userDto, currentUser);
            return updateResult.IsSuccess ? Ok(updateResult) : BadRequest(updateResult);
        }
        #endregion

        #region Edit User
        /// <summary>
        /// Edit  user
        /// </summary>
        /// <returns>Status of action</returns>
        [HttpPut]
        public async Task<ActionResult> EditUser(int userId, EditUserDto model)
        {
            var currentUser = HttpContext.User;
            var editResult = await _userService.EditUser(userId, model, currentUser);
            return editResult.IsSuccess ? Ok(editResult) : BadRequest(editResult);
        }
        #endregion


        #region Delete user
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <returns>Status of action</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto request)
        {
            var currentUser = HttpContext.User;
            var result = await _userService.DeleteUser(request, currentUser);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        //private bool UserExists(int uid)
        //{
        //    //return _context.Kois.Any(e => e.KoiId == id);
        //    return _unitOfWork.UserRepository.GetByIdAsync(uid) == null;
        //}
    }
}