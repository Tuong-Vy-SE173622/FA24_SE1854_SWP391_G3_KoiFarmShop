using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestCreateDto
    {
        public int? CustomerId { get; set; }
        public string? SubAmount { get; set; }
        public string? Vat { get; set; }
        public string? VatAmount { get; set; }
        public int? PromotionAmount { get; set; }
        public double? TotalAmount { get; set; }
        public double? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public bool? IsOnline { get; set; }
    }
}
