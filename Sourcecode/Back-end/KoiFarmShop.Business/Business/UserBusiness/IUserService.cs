using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data.Models;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.UserBusiness
{
    public interface IUserService
    {
        public User GetUserById(int uid);
        public User GetUserByUserName(string userName);
        public Task<ResultDto> GetUserList(int? userId, string? name, string? email, string? phone, string? firstName, string? lastName);
        public Task<ResultDto> SearchUserByKeyword(string keyword);
        public Task<ResultDto> UpdateUser(int userId, UserDto userDto, ClaimsPrincipal userUpdate);
        public Task<ResultDto> EditUser(int userId, EditUserDto edit, ClaimsPrincipal userUpdate);
        public Task<ResultDto> DeleteUser(DeleteUserDto request, ClaimsPrincipal userDelete);
    }
}
