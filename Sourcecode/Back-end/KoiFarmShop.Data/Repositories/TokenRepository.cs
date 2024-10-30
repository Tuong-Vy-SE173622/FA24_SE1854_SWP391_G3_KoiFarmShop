using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Data.Repositories
{
    public class TokenRepository : GenericRepository<Token>
    {
        public TokenRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) : base(context)
        {

        }
    }
}
