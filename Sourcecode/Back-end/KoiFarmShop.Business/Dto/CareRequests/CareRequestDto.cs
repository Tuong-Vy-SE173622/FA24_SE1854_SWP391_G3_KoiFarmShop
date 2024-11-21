using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequest;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestDto
    {
        public int CareRequestId { get; set; }

        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }

        public int? CarePlanId { get; set; }

        public DateTime? StartDate { get; set; }

        public CareRequestStatus? Status { get; set; }
        public decimal? TotalAmount { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public CarePlan? CarePlan { get; set; }

        public ICollection<CareRequestDetail>? CareRequestDetail { get; set; }
    }
}
