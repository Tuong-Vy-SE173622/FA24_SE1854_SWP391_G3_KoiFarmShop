using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class ConsignmentRequestRepository : GenericRepository<ConsignmentRequest>
    {
        public ConsignmentRequestRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<ConsignmentRequest> GetRequestWithDetailsAsync(int consignmentId)
        {
            return await _context.ConsignmentRequests
                .Include(c => c.ConsignmentDetails)
                .FirstOrDefaultAsync(c => c.ConsignmentId == consignmentId);
        }
        public async Task<bool> HasAnyAssociatedDetails(int consignmentId)
        {
            return await _context.ConsignmentDetails.AnyAsync(c => c.ConsignmentId == c.ConsignmentId);
        }
    }
}
