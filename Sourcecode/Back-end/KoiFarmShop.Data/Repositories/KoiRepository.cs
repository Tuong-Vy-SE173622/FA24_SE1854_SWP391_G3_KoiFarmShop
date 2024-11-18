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
            //get user with that id
            var user = _context.Users.Where( u => u.UserId == userId).FirstOrDefault();
            if (user == null) return [];
            // get kois created by that user name
            return await _context.Kois.Where(k => k.CreatedBy == user.Username)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
