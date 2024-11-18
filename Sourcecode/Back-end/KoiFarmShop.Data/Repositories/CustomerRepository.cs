using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers
                .Select(x => new Customer
                {
                    CustomerId = x.CustomerId,
                    UserId = x.UserId,
                    Address = x.Address,
                    LoyaltyPoints = x.LoyaltyPoints,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy

                })
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Customer?> GetUserIdOfCustomer(int customerId)
        {
            return await _context.Customers.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
        }
    }
}
