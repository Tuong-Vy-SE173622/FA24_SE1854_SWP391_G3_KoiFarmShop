﻿using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data.Repositories
{
    public class TokenRepository : GenericRepository<Token>
    {
        public TokenRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) : base(context)
        {

        }
    }
}
