using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class KoiRepository : GenericRepository<Koi>
    {
        public KoiRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<List<Koi>> GetAllKoisCreatedByUser(int userId)
        {
            var user = await _context.Users
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return new List<Koi>(); // Return an empty list if the user doesn't exist

            // Get Kois created by the specified username
            return await _context.Kois
                .Where(k => k.CreatedBy == user.Username) 
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Koi?> GetKoiWithConsignment(int koiId)
        {
            return await _context.Kois.Include(k => k.ConsignmentRequest) 
                .AsNoTracking() 
                .FirstOrDefaultAsync(k => k.KoiId == koiId);
        }
    }
}
