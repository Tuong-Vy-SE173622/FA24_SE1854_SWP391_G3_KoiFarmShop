using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequest;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestCreateDto
    {

        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }
        public int? CarePlanId { get; set; }

        public string? Note { get; set; }

        public string? CreatedBy { get; set; }
    }
}
