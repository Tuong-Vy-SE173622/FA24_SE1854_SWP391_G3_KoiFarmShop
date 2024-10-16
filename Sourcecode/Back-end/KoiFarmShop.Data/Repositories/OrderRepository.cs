using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
