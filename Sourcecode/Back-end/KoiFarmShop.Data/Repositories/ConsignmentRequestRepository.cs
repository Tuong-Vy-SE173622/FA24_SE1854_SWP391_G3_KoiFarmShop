using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class ConsignmentRequestRepository : GenericRepository<ConsignmentRequest>
    {
        public ConsignmentRequestRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<ConsignmentRequest> GetRequestWithTransactionAsync(int consignmentId)
        {
            return await _context.ConsignmentRequests
                .Include(c => c.ConsignmentTransaction)
                .FirstOrDefaultAsync(c => c.ConsignmentId == consignmentId);
        }
    }
}
