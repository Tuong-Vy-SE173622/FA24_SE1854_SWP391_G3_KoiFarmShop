using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto
{
    public class OrderDto
    {
        [Key]
        public int OrderId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        public double? SubAmount { get; set; }

        public double? Vat { get; set; }

        public double? VatAmount { get; set; }

        public double? PromotionAmount { get; set; }

        public double? TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public bool? IsActive { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
        public CustomerDto? Customer { get; set; }
    }
}
