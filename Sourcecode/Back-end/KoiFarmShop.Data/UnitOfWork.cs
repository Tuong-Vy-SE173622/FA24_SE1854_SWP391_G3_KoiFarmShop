//using KoiFarmShop.Data.Models;
//using KoiFarmShop.Data.Repositories;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KoiFarmShop.Data
//{
//    public class UnitOfWork : IDisposable
//    {
//        private FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
//        private const string ErrorOrder = "Error!";
//        private const string ErrorRequest = "Error!";
//        private bool isOrder;
//        private bool isRequest;
//        private bool _disposed;


//        private KoiRepository _koiRepository;
//        private KoiTypeRepository _koiTypeRepository;
//        private OrderRepository _orderRepository;
//        private OrderItemRepository _orderItemRepository;
//        private CustomerRepository _customerRepository;
//        private UserRepository _userRepository;
//        private ConsignmentRequestRepository _consignmentRequestRepository;
//        private ConsignmentDetailRepository _consignmentDetailRepository;
//        private CareRequestRepository _careRequestRepository;
//        private CareRequestDetailRepository _careRequestDetailRepository;

//        public UnitOfWork() => _context ??= new FA_SE1854_SWP391_G3_KoiFarmShopContext();

//        internal FA_SE1854_SWP391_G3_KoiFarmShopContext Context => _context;

//        public KoiRepository KoiRepository => 
//            _koiRepository ??= new KoiRepository(this);

//        public KoiTypeRepository KoiTypeRepository => 
//            _koiTypeRepository ??= new KoiTypeRepository(this);

//        public OrderRepository OrderRepository =>
//           _orderRepository ??= new OrderRepository(this);
//        public OrderItemRepository OrderItemRepository =>
//           _orderItemRepository ??= new OrderItemRepository(this);
//        public CustomerRepository CustomerRepository =>
//           _customerRepository ??= new CustomerRepository(this);
//        public UserRepository UserRepository =>
//           _userRepository ??= new UserRepository(this);
//        public ConsignmentRequestRepository ConsignmentRequestRepository =>
//           _consignmentRequestRepository ??= new ConsignmentRequestRepository(this);
//        public ConsignmentDetailRepository ConsignmentDetailRepository =>
//           _consignmentDetailRepository ??= new ConsignmentDetailRepository(this);

//        public bool IsOrder
//        {
//            get
//            {
//                return this.isOrder;
//            }
//        }

//        public bool IsRequest
//        {
//            get
//            {
//                return this.isRequest;
//            }
//        }

//        public async Task BeginOrderAsync()
//        {
//            if (this.isOrder)
//            {
//                throw new Exception(ErrorOrder);
//            }

//            isOrder = true;
//        }

//        public async Task CommitOrderAsync()
//        {
//            if (!this.isOrder)
//            {
//                throw new Exception(ErrorOrder);
//            }

//            await this._context.SaveChangesAsync();
//            this.isOrder = false;
//        }

//        public async Task RollbackOrderAsync()
//        {
//            if (!this.isOrder)
//            {
//                throw new Exception(ErrorOrder);
//            }

//            this.isOrder = false;

//            foreach (var entry in this._context.ChangeTracker.Entries())
//            {
//                entry.State = EntityState.Detached;
//            }
//        }

//        public async Task BeginRequestAsync()
//        {
//            if (this.isRequest)
//            {
//                throw new Exception(ErrorRequest);
//            }

//            isRequest = true;
//        }

//        public async Task CommitRequestAsync()
//        {
//            if (!this.isRequest)
//            {
//                throw new Exception(ErrorRequest);
//            }

//            await this._context.SaveChangesAsync();
//            this.isRequest = false;
//        }

//        public async Task RollbackRequestAsync()
//        {
//            if (!this.isRequest)
//            {
//                throw new Exception(ErrorRequest);
//            }

//            this.isRequest = false;

//            foreach (var entry in this._context.ChangeTracker.Entries())
//            {
//                entry.State = EntityState.Detached;
//            }
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!_disposed)
//                if (disposing)
//                    this._context.Dispose();

//            _disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}

using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KoiFarmShop.Data
{
    public class UnitOfWork : IDisposable
    {
        private FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private const string ErrorTransaction = "Transaction error!";
        private bool _isTransactionActive;
        private TransactionType _currentTransactionType;
        private bool _disposed;

        private KoiRepository _koiRepository;
        private KoiTypeRepository _koiTypeRepository;
        private OrderRepository _orderRepository;
        private OrderItemRepository _orderItemRepository;
        private CustomerRepository _customerRepository;
        private UserRepository _userRepository;
        private ConsignmentRequestRepository _consignmentRequestRepository;
        private ConsignmentDetailRepository _consignmentDetailRepository;
        private CareRequestRepository _careRequestRepository;
        private CareRequestDetailRepository _careRequestDetailRepository;

        public UnitOfWork() => _context ??= new FA_SE1854_SWP391_G3_KoiFarmShopContext();

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

        public CareRequestRepository CareRequestRepository =>
            _careRequestRepository ??= new CareRequestRepository(this);

        public CareRequestDetailRepository CareRequestDetailRepository =>
            _careRequestDetailRepository ??= new CareRequestDetailRepository(this);

        public async Task BeginTransactionAsync()
        {
            if (this._isTransactionActive)
            {
                throw new Exception(ErrorTransaction);
            }

            _isTransactionActive = true;
        }

        public async Task CommitTransactionAsync()
        {
            if (!_isTransactionActive)
            {
                throw new Exception(ErrorTransaction);
            }

            await _context.SaveChangesAsync();
            _isTransactionActive = false;
        }

        public async Task RollbackTransactionAsync()
        {
            if (!_isTransactionActive)
            {
                throw new Exception(ErrorTransaction);
            }

            _isTransactionActive = false;

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public enum TransactionType
    {
        Order,
        ConsignmentRequest,
        CareRequest
    }
}

