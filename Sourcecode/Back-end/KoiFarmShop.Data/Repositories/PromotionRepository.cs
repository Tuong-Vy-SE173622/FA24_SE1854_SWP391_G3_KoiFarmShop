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
    public class PromotionRepository : GenericRepository<Promotion>
    {
        private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        public PromotionRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Promotion>> GetPromotions()
        {
            return await _context.Promotions
                .Select(x => new Promotion
                {
                    PromotionId = x.PromotionId,
                    Description = x.Description,
                    DiscountPercentage = x.DiscountPercentage,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsActive = x.IsActive,
                    Note = x.Note,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy

                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
