using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class KoiTypeRepository : GenericRepository<KoiType>
    {
        public KoiTypeRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<bool> HasAssociatedKoi(int koiTypeId)
        {
            return await _context.Kois.AnyAsync(k => k.KoiTypeId == koiTypeId);
            
        }
    }
}
