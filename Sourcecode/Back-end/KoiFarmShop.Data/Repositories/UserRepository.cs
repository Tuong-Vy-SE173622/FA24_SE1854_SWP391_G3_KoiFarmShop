using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
