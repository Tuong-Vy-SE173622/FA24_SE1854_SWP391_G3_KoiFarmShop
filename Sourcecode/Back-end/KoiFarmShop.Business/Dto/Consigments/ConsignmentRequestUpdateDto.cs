using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestUpdateDto
    {
        public int? CustomerId { get; set; }

        [StringLength(50, ErrorMessage = "PaymentMethod cannot be longer than 50 characters.")]
        public string? PaymentMethod { get; set; }

        [StringLength(50, ErrorMessage = "PaymentStatus cannot be longer than 50 characters.")]
        public string? PaymentStatus { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(255, ErrorMessage = "Note cannot be longer than 255 characters.")]
        public string? Note { get; set; }

        [StringLength(255, ErrorMessage = "Status cannot be longer than 255 characters.")]
        public string? Status { get; set; }

        public bool? IsOnline { get; set; }
    }

}
