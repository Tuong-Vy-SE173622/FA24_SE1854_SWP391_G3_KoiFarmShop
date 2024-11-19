using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class AccountRepository : GenericRepository<User>
    {
        private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        public AccountRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> GenerateNewUserId()
        {
            const int maxRetryCount = 5;
            int retryCount = 0;

            while (retryCount < maxRetryCount)
            {
                var lastUser = await _context.Users
                                             .OrderByDescending(u => u.UserId)
                                             .FirstOrDefaultAsync();

                if (lastUser == null || lastUser.UserId == 0)
                {
                    return 1; // Start from 1 (integer value)
                }

                int newId = lastUser.UserId + 1;

                // Check if the generated userId already exists
                var existingUser = await _context.Users
                                                 .Where(u => u.UserId == newId)
                                                 .FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return newId; // Return the new integer ID
                }

                // Increment retry count and try again
                retryCount++;
            }

            throw new InvalidOperationException("Failed to generate a unique UserId after multiple attempts.");
        }
        public async Task<int> GenerateNewCustomerId()
        {
            const int maxRetryCount = 5;
            int retryCount = 0;

            while (retryCount < maxRetryCount)
            {
                var lastCustomer = await _context.Customers
                                             .OrderByDescending(c => c.CustomerId)
                                             .FirstOrDefaultAsync();

                if (lastCustomer == null || lastCustomer.CustomerId == 0)
                {
                    return 1; // Start from 1 (integer value)
                }

                int newId = lastCustomer.CustomerId + 1;

                // Check if the generated userId already exists
                var existingCustomer = await _context.Customers
                                                 .Where(c => c.CustomerId == newId)
                                                 .FirstOrDefaultAsync();

                if (existingCustomer == null)
                {
                    return newId; // Return the new integer ID
                }

                // Increment retry count and try again
                retryCount++;
            }

            throw new InvalidOperationException("Failed to generate a unique UserId after multiple attempts.");
        }
    }
}
