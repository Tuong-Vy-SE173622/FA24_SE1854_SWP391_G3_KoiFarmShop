﻿using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data.Repositories
{
    public class UserRepository: GenericRepository<User>
    {
        private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        public UserRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) : base(context)
        {
            _context = context;
        }
            
        public async Task<List<User>> GetAllUsers()
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
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    Role = x.Role
                })
                .AsNoTracking()
                .ToListAsync();
        }
        public User GetUserById(int uid)
        {
            try
            {
                var user = Get(x => x.UserId == uid);

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
            var user = Get(u => u.Username == userName);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
