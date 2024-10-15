using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestResponseDto
    {
        public int ConsignmentId { get; set; }
        public int? CustomerId { get; set; }
        public string? Status { get; set; }
        public double? TotalAmount { get; set; }
        public bool? IsActive { get; set; }
        public List<ConsignmentDetailResponseDto> ConsignmentDetails { get; set; }
    }
}
