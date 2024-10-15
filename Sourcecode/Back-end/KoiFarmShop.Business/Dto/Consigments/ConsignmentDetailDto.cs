﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentDetailDto
    {
        public int ConsignmentDetailId { get; set; }
        public int? KoiId { get; set; }
        public string? ConsignmentType { get; set; }
        public double? MonthlyConsignmentFee { get; set; }
        public double? SoldPrice { get; set; }
        public string? HealthDescription { get; set; }
        public double? Weight { get; set; }
        public string? Status { get; set; }
    }
}
