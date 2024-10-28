using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class KoiRepository : GenericRepository<Koi>
    {
        public KoiRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;
        //public async Task<int> GetLastKoiIdAsync()
        //{
        //    // Example query to get the last Koi
        //    var lastKoi = await _context.Kois.OrderByDescending(s => s.KoiId).FirstOrDefaultAsync();

        //    // Check if lastKoi is null
        //    if (lastKoi == null)
        //    {
        //        return 0; // or throw an exception, depending on your needs
        //    }

        //    return lastKoi.KoiId;
        //}
    }
}
