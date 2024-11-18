﻿using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class ConsignmentRequestRepository : GenericRepository<ConsignmentRequest>
    {
        public ConsignmentRequestRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<ConsignmentRequest?> GetRequestWithTransactionAsync(int consignmentId)
        {
            return await _context.ConsignmentRequests
                .Include(c => c.ConsignmentTransaction)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ConsignmentId == consignmentId);
        }
        public async Task<List<ConsignmentRequest>> GetAllConsignmentByCustomer(int customerId)
        {
            return await _context.ConsignmentRequests.Where(c => c.CustomerId == customerId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
