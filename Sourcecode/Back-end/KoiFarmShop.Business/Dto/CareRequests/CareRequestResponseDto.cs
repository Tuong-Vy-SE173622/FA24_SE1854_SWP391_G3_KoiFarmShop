using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestResponseDto
    {
        public int RequestId { get; set; }

        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }

        public string? Status { get; set; }

        public bool? IsActive { get; set; }

        public List<CareRequestDetailDto> CareRequestDetailsDto { get; set; }

    }
}
