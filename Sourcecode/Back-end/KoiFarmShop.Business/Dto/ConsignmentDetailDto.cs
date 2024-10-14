using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class ConsignmentDetailDto
    {
        public int ConsignmentDetailId { get; set; }

        public int? ConsignmentId { get; set; }

        public int? KoiId { get; set; }

        public string ConsignmentType { get; set; }

        public double? MonthlyConsignmentFee { get; set; }

        public double? SoldPrice { get; set; }

        public string HealthDescription { get; set; }

        public double? Weight { get; set; }

        public string Status { get; set; }

        public bool? IsActive { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public  ConsignmentRequestDto Consignment { get; set; }

        public KoiDto Koi { get; set; }
    }
}
