using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Data.Repositories
{
    public class ConsignmentDetailRepository : GenericRepository<ConsignmentDetail>
    {
        public ConsignmentDetailRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;
    }
}
