using KoiFarmShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.AccountBusiness
{
    public interface IAccountService
    {
        public Task<ResultDto> AddNewUser(RegisterDto model, ClaimsPrincipal userCreate);
    }
}
