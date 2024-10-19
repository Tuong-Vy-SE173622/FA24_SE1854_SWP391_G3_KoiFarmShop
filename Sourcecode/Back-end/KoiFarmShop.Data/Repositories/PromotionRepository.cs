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
        public async Task<int> GenerateNewPromotionId()
        {
            const int maxRetryCount = 5;
            int retryCount = 0;

            while (retryCount < maxRetryCount)
            {
                var lastPromotion = await _context.Promotions
                                             .OrderByDescending(u => u.PromotionId)
                                             .FirstOrDefaultAsync();

                if (lastPromotion == null || lastPromotion.PromotionId == 0)
                {
                    return 1; // Start from 1 (integer value)
                }

                int newId = lastPromotion.PromotionId + 1;

                // Check if the generated userId already exists
                var existingPromotion = await _context.Promotions
                                                 .Where(u => u.PromotionId == newId)
                                                 .FirstOrDefaultAsync();

                if (existingPromotion == null)
                {
                    return newId; // Return the new integer ID
                }

                // Increment retry count and try again
                retryCount++;
            }

            throw new InvalidOperationException("Failed to generate a unique UserId after multiple attempts.");
        }
    }
}
