using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentDetailUpdateDto
    {
        public int ConsignmentDetailId { get; set; }
        public double? MonthlyConsignmentFee { get; set; }
        public double? SoldPrice { get; set; }
        public string? HealthDescription { get; set; }
        public double? Weight { get; set; }
    }
}
