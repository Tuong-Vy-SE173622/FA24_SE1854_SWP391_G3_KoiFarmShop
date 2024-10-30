using KoiFarmShop.Business.Dto;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.AccountBusiness
{
    public interface IAccountService
    {
        public Task<ResultDto> AddNewUser(RegisterDto model, ClaimsPrincipal userCreate);
    }
}
