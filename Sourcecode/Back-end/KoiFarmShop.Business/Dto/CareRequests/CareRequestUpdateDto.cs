using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequest;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestUpdateDto
    {
        //public int RequestId { get; set; }
        public int? CarePlanId { get; set; }
        public CareRequestStatus? Status { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }

        //public List<CareRequestDetailDto>? CareRequestDetailsDto { get; set; }
    }
}
