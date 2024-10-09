using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.UserBusiness
{
    public interface IUserService
    {
        public User GetUserById(int uid);
        public User GetUserByUserName(string userName);
    }
}
