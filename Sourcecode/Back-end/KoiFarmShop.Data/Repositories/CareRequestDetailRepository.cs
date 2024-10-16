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
    public class CareRequestDetailRepository : GenericRepository<CareRequestDetail>
    {
        public CareRequestDetailRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<CareRequestDetail>> GetAllAsync(Expression<Func<CareRequestDetail, bool>> filter = null)
        {
            return await _dbSet.Include(x => x.Request).ToListAsync();
        }
    }
}
