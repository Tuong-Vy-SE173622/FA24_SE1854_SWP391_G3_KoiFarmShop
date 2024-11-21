using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class KoiRepository : GenericRepository<Koi>
    {
        public KoiRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<List<Koi>> GetAllKoisCreatedByUser(int userId, bool? isInConsignment, bool? isInCareRequest)
        {
            var user = await _context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return []; 

            
            var koiQuery = _context.Kois
                .AsNoTracking()
                .Where(k => k.CreatedBy == user.Username); 

            // Apply consignment filter if isInConsignment is not null
            if (isInConsignment.HasValue)
            {
                if (isInConsignment.Value)
                {
                    koiQuery = koiQuery.Where(k => _context.ConsignmentRequests
                        .Any(cr => cr.KoiId == k.KoiId)); 
                }
                else
                {
                    koiQuery = koiQuery.Where(k => !_context.ConsignmentRequests
                        .Any(cr => cr.KoiId == k.KoiId)); 
                }
            }

            // Apply care request filter if isInCareRequest is not null
            if (isInCareRequest.HasValue)
            {
                if (isInCareRequest.Value)
                {
                    koiQuery = koiQuery.Where(k => _context.CareRequests
                        .Any(cr => cr.KoiId == k.KoiId)); 
                }
                else
                {
                    koiQuery = koiQuery.Where(k => !_context.CareRequests
                        .Any(cr => cr.KoiId == k.KoiId)); 
                }
            }

            // Return the filtered list of Kois
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
