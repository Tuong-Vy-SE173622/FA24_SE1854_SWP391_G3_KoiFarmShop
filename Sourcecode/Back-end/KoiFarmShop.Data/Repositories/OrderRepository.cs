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
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.OrderId == orderId);
        }

        public async Task<double?> SumOfOrderItem(int orderId)
        {
            return await _context.OrderItems
                .Where(item => item.OrderId == orderId)
                .SumAsync(item => item.Price * item.Amount);
        }
    }
}
