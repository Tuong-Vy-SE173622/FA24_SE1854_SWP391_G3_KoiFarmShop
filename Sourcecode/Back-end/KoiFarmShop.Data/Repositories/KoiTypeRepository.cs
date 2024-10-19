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
    public class KoiTypeRepository : GenericRepository<KoiType>
    {
        public KoiTypeRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) => _context = context;
    }
}
