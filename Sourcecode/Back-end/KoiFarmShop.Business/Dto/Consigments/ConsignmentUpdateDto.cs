using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentUpdateDto
    {
        public int ConsignmentId { get; set; }
        public string? Status { get; set; }
        public double? TotalAmount { get; set; }
        public List<ConsignmentDetailUpdateDto> ConsignmentDetails { get; set; }
    }
}
