using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
