namespace KoiFarmShop.Business.Dto.Promotion
{
    public class PromotionCreateDto
    {
        public string? Description { get; set; }
        public double? DiscountPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }
    }
}
