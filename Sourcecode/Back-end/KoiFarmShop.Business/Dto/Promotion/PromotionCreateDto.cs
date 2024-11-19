using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Promotion
{
    public class PromotionCreateDto
    {
        public string? Description { get; set; }
        [Required]
        public double DiscountPercentage { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? Note { get; set; }
    }
}
