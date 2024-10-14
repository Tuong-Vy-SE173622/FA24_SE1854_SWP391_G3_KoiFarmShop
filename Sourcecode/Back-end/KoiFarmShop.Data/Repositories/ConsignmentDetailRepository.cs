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
    public class ConsignmentDetailRepository : GenericRepository<ConsignmentDetail>
    {
        public ConsignmentDetailRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<ConsignmentDetail>> GetAllAsync(Expression<Func<ConsignmentDetail, bool>> filter = null)
        {
            return await _dbSet.Include(x => x.Consignment)
                .Include(x => x.Koi)
                .ToListAsync();
        }
    }
}
