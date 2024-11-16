namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestResponseDto
    {
        public int? ConsignmentId { get; set; }
        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }

        public decimal? ArgredSalePrice { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }
    }
}
