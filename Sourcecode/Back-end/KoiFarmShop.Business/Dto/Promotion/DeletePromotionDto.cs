using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Promotion
{
    public class DeletePromotionDto
    {
        [Required(ErrorMessage = "PromotionId is required")]
        [Display(Name = "Promotion Id")]
        public required int PromotionId { get; set; }
    }
}
