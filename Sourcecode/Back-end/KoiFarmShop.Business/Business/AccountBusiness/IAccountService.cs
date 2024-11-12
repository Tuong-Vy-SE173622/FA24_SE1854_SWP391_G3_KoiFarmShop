using KoiFarmShop.Business.Dto;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.AccountBusiness
{
    public interface IAccountService
    {
        Task<ResultDto> AddNewUser(RegisterDto model, ClaimsPrincipal userCreate);
        Task<ResultDto> AddNewCustomer(CustomerDto model, ClaimsPrincipal userCreate);
    }
}
