using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Payment
{
    public class PaymentRequestForConsignment
    {

        [Required]
        public string ConsignmentId { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

    }
}