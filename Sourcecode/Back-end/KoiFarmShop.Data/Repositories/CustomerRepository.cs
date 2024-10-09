using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
