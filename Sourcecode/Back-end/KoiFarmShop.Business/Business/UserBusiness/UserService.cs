using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.UserBusiness
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User GetUserById(int uid)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Get(x => x.UserId == uid);

                if (user == null)
                {
                    // Handle the case where the user is not found, e.g., return null or throw an exception
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public User GetUserByUserName(string userName)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Username == userName);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
