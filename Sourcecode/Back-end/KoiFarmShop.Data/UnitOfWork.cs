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
        private UserRepository _userRepository;
        private TokenRepository _tokenRepository;
        private AccountRepository _accountRepository;
        private OrderItemRepository _orderItemRepository;
        private CustomerRepository _customerRepository;
        private UserRepository _userRepository;
        private ConsignmentRequestRepository _consignmentRequestRepository;
        private ConsignmentDetailRepository _consignmentDetailRepository;
        private CareRequestRepository _careRequestRepository;
        private CareRequestDetailRepository _careRequestDetailRepository;

        public UnitOfWork() => _context ??= new FA_SE1854_SWP391_G3_KoiFarmShopContext();

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }
        public TokenRepository TokenRepository
        {
            get
            {
                return _tokenRepository ??= new TokenRepository(_context);
            }
        }
        public KoiRepository KoiRepository
        {
            get
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
        public KoiTypeRepository KoiTypeRepository
        {
            get
            {
                return (_koiTypeRepository ??= new KoiTypeRepository(_context));
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {
                return _orderRepository ??= new OrderRepository(_context);
            }
        }
        public AccountRepository AccountRepository
        {
            get
            {
                return _accountRepository ??= new AccountRepository(_context);
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

