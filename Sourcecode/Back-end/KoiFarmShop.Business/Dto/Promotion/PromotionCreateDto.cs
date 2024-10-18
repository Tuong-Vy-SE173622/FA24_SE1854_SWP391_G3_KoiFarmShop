using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Promotion
{
    public class PromotionCreateDto
    {
        public string? Description { get; set; }
        public double? DiscountPercentage { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }
    }
}
