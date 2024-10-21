﻿namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestResponseDto
    {
        public int ConsignmentId { get; set; }
        public int CustomerId { get; set; }
        public string? Status { get; set; }
        public double? SubAmount { get; set; }
        public double? Vat { get; set; }
        public double? VatAmount { get; set; }
        public double? PromotionAmount { get; set; }
        public double? TotalAmount { get; set; }
        public bool? IsActive { get; set; }
        public List<ConsignmentDetailResponseDto>? ConsignmentDetails { get; set; }
    }
}
