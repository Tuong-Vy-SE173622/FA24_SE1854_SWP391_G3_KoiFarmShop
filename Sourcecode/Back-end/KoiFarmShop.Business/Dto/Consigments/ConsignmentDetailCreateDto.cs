using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentDetailCreateDto
    {
        public int? KoiId { get; set; }
        public string? ConsignmentType { get; set; }
        public double? MonthlyConsignmentFee { get; set; }
        public double? SoldPrice { get; set; }
        public string? HealthDescription { get; set; }
        public double? Weight { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }
    }
}
