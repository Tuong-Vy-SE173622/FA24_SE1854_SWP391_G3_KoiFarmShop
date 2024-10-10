using KoiFarmShop.Data.Models;
using KoiFarmShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Business.Business.UserBusiness;
using KoiFarmShop.Business.Dto;

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

        //public KoisController(FA_SE1854_SWP391_G3_KoiFarmShopContext context)
        //{
        //    _context = context;
        //}


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
        public async Task<ActionResult<ResultDto>> UpdateUser(int userId, UserDto userDto)
        {
            try
            {
                var currentUser = HttpContext.User;
                var updateResult = await _userService.UpdateUser(userId, userDto, currentUser);
                var result = new ResultDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Data = updateResult
                };
                return Ok(result);
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

        //// POST: api/Kois
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    //_context.Kois.Add(koi);
        //    try
        //    {
        //        //await _context.SaveChangesAsync();
        //        await _unitOfWork.UserRepository.CreateAsync(user);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserExists(user.UserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUser", new { uid = user.UserId }, user);
        //}

        //// DELETE: api/Kois/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    //var koi = await _context.Kois.FindAsync(id);
        //    var koi = await _unitOfWork.UserRepository.GetByIdAsync(id);
        //    if (koi == null)
        //    {
        //        return NotFound();
        //    }

        //    await _unitOfWork.UserRepository.SaveAsync();

        //    //_context.Kois.Remove(koi);
        //    //await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool UserExists(int uid)
        //{
        //    //return _context.Kois.Any(e => e.KoiId == id);
        //    return _unitOfWork.UserRepository.GetByIdAsync(uid) == null;
        //}
    }
}