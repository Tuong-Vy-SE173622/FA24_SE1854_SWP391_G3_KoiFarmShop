﻿namespace KoiFarmShop.Business.Dto.Promotion
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public string? Description { get; set; }
        public double? DiscountPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
