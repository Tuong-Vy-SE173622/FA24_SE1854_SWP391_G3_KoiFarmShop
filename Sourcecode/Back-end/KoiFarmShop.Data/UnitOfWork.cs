using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data
{
    public class UnitOfWork
    {
        private FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private KoiRepository _koiRepository;
        private KoiTypeRepositories _koiTypeRepositories;
        private OrderRepository _orderRepository;

        public UnitOfWork() => _context ??= new FA_SE1854_SWP391_G3_KoiFarmShopContext();

        public KoiRepository KoiRepository
        {
            get
            {
                return _koiRepository ??= new KoiRepository(_context);
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {
                return _orderRepository ??= new OrderRepository(_context);
            }
        }
    }
}
