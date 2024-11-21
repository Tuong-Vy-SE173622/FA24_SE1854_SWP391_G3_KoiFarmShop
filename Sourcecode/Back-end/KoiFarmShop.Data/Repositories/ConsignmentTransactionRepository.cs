﻿using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data.Repositories
{
    public class ConsignmentTransactionRepository : GenericRepository<ConsignmentTransaction>
    {
        public ConsignmentTransactionRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<List<ConsignmentTransaction>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.ConsignmentTransactions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
