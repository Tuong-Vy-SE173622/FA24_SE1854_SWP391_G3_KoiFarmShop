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
    public class CareRequestRepository : GenericRepository<CareRequest>
    {
        public CareRequestRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<CareRequest> GetCareRequestWithDetailsAsync(int careId)
        {
            return await _context.CareRequests
                .Include(c => c.CareRequestDetails)
                .FirstOrDefaultAsync(c => c.CareRequestId == careId);
        }
    }
}
