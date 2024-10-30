using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Data.Repositories
{
    public class KoiRepository : GenericRepository<Koi>
    {
        public KoiRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;
    }
}
