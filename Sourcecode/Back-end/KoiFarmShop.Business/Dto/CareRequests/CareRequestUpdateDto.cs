using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestUpdateDto
    {
        //public int RequestId { get; set; }

        public string? Status { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }
        public string? UpdatedBy { get; set; }

        //public List<CareRequestDetailDto>? CareRequestDetailsDto { get; set; }
    }
}
