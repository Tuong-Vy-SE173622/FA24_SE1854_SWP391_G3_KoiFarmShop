using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>
    {
        public OrderItemRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<OrderItem>> GetAllAsync(Expression<Func<OrderItem, bool>> filter = null)
        {
            return await _dbSet.Include(x => x.Order)
                .Include(x => x.Koi)
                .ToListAsync();
        }
    }
}
