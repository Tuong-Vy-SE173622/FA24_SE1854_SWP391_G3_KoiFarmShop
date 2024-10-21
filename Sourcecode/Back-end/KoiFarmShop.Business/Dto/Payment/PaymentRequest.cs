using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Payment
{
    public class PaymentRequest
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required]
        public string OrderDescription { get; set; }

        [Required]
        public string OrderId { get; set; }

        public string? BankCode { get; set; }
    }
}

