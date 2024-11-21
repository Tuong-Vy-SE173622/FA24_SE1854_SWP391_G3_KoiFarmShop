using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class KoiRepository : GenericRepository<Koi>
    {
        public KoiRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<List<Koi>> GetAllKoisCreatedByUser(int userId, bool isInConsignment, bool isInCareRequest)
        {
            var user = await _context.Users
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return []; // Return an empty list if the user doesn't exist

            // Query to get Koi created by the user
            var koiQuery = _context.Kois
                .AsNoTracking()
                .Where(k => k.CreatedBy == user.Username);

            if (isInConsignment)
            {
                koiQuery = koiQuery.Where(k => _context.ConsignmentRequests
                    .Any(cd => cd.KoiId == k.KoiId));
            }

            if (isInCareRequest)
            {
                koiQuery = koiQuery.Where(k => _context.CareRequests
                    .Any(crd => crd.KoiId == k.KoiId));
            }

            return await koiQuery.ToListAsync();
        }

        public async Task<Koi?> GetKoiWithConsignment(int koiId)
        {
            return await _context.Kois.Include(k => k.ConsignmentRequest) 
                .AsNoTracking() 
                .FirstOrDefaultAsync(k => k.KoiId == koiId);
        }

        public async Task<int> CountAllKoisAsync() { return await _context.Kois.CountAsync(); }
    }
}
