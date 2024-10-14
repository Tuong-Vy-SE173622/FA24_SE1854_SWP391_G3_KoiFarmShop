using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
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
        private const string ErrorOrder = "Error!";
        private const string ErrorRequest = "Error!";
        private bool isOrder;
        private bool isRequest;
        private bool _disposed;


        private KoiRepository _koiRepository;
        private KoiTypeRepository _koiTypeRepository;
        private OrderRepository _orderRepository;
        private OrderItemRepository _orderItemRepository;
        private CustomerRepository _customerRepository;
        private UserRepository _userRepository;
        private ConsignmentRequestRepository _consignmentRequestRepository;
        private ConsignmentDetailRepository _consignmentDetailRepository;

        public UnitOfWork() => _context ??= new FA_SE1854_SWP391_G3_KoiFarmShopContext();
        //public UnitOfWork(FA_SE1854_SWP391_G3_KoiFarmShopContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}
        
        internal FA_SE1854_SWP391_G3_KoiFarmShopContext Context => _context;

        public KoiRepository KoiRepository => 
            _koiRepository ??= new KoiRepository(this);

        public KoiTypeRepository KoiTypeRepository => 
            _koiTypeRepository ??= new KoiTypeRepository(this);

        public OrderRepository OrderRepository =>
           _orderRepository ??= new OrderRepository(this);
        public OrderItemRepository OrderItemRepository =>
           _orderItemRepository ??= new OrderItemRepository(this);
        public CustomerRepository CustomerRepository =>
           _customerRepository ??= new CustomerRepository(this);
        public UserRepository UserRepository =>
           _userRepository ??= new UserRepository(this);
        public ConsignmentRequestRepository ConsignmentRequestRepository =>
           _consignmentRequestRepository ??= new ConsignmentRequestRepository(this);
        public ConsignmentDetailRepository ConsignmentDetailRepository =>
           _consignmentDetailRepository ??= new ConsignmentDetailRepository(this);

        public bool IsOrder
        {
            get
            {
                return this.isOrder;
            }
        }

        public bool IsRequest
        {
            get
            {
                return this.isRequest;
            }
        }

        public async Task BeginOrderAsync()
        {
            if (this.isOrder)
            {
                throw new Exception(ErrorOrder);
            }

            isOrder = true;
        }

        public async Task CommitOrderAsync()
        {
            if (!this.isOrder)
            {
                throw new Exception(ErrorOrder);
            }

            await this._context.SaveChangesAsync();
            this.isOrder = false;
        }

        public async Task RollbackOrderAsync()
        {
            if (!this.isOrder)
            {
                throw new Exception(ErrorOrder);
            }

            this.isOrder = false;

            foreach (var entry in this._context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        public async Task BeginRequestAsync()
        {
            if (this.isRequest)
            {
                throw new Exception(ErrorRequest);
            }

            isRequest = true;
        }

        public async Task CommitRequestAsync()
        {
            if (!this.isRequest)
            {
                throw new Exception(ErrorRequest);
            }

            await this._context.SaveChangesAsync();
            this.isRequest = false;
        }

        public async Task RollbackRequestAsync()
        {
            if (!this.isRequest)
            {
                throw new Exception(ErrorRequest);
            }

            this.isRequest = false;

            foreach (var entry in this._context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    this._context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
