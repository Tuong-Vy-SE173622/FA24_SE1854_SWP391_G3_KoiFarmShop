using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;

namespace KoiFarmShop.Data
{
    public class UnitOfWork
    {
        private FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private KoiRepository _koiRepository;
        private KoiTypeRepository _koiTypeRepository;
        private OrderRepository _orderRepository;
        private UserRepository _userRepository;
        private TokenRepository _tokenRepository;
        private AccountRepository _accountRepository;
        private ConsignmentDetailRepository _consignmentDetailRepository;
        private ConsignmentRequestRepository _consignmentRequestRepository;
        private PromotionRepository _promotionRepository;

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
            {
                return _koiRepository ??= new KoiRepository(_context);
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

        public ConsignmentDetailRepository ConsignmentDetailRepository
        {
            get
            {
                return _consignmentDetailRepository ??= new ConsignmentDetailRepository(_context);
            }
        }

        public ConsignmentRequestRepository ConsignmentRequestRepository
        {
            get
            {
                return _consignmentRequestRepository ??= new ConsignmentRequestRepository(_context);
            }
        }

        public PromotionRepository PromotionRepository
        {
            get
            {
                return _promotionRepository ??= new PromotionRepository(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
