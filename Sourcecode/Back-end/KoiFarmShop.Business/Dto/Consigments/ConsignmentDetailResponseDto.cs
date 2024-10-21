namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentDetailResponseDto
    {
        public int ConsignmentDetailId { get; set; }
        public int ConsignmentId { get; set; }
        public int? KoiId { get; set; }
        public string? ConsignmentType { get; set; }
        public double? MonthlyConsignmentFee { get; set; }
        public double? SoldPrice { get; set; }
        public string? HealthDescription { get; set; }
        public double? Weight { get; set; }
    }
}
