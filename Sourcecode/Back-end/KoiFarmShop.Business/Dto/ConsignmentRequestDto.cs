using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class ConsignmentRequestDto
    {
        public int ConsignmentId { get; set; }

        public int CustomerId { get; set; }

        public double? SubAmount { get; set; }

        public double? Vat { get; set; }

        public double? VatAmount { get; set; }

        public double? PromotionAmount { get; set; }

        public double? TotalAmount { get; set; }

        public double? PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public bool? IsActive { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public bool? IsOnline { get; set; }

        public CustomerDto Customer { get; set; }
    }
}
