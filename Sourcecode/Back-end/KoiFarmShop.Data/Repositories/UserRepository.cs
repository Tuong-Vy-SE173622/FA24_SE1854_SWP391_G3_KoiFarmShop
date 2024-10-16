using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        public UserRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users
                .Select(x => new User
                {
                    UserId = x.UserId,
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    IsActive = x.IsActive,
                    Note = x.Note,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    Role = x.Role,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                   
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
