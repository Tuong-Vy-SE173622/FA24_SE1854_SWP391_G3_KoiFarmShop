using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Promotion
{
    public class DeletePromotionDto
    {
        [Required(ErrorMessage = "PromotionId is required")]
        [Display(Name = "Promotion Id")]
        public required int PromotionId { get; set; }
    }
}
