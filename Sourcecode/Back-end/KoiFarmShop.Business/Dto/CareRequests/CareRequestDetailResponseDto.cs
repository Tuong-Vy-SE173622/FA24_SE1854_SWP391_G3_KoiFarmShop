using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestDetailResponseDto
    {
        public int RequestDetailId { get; set; }

        public int? Image { get; set; }

        public int? CareMethod { get; set; }

        public string? Note { get; set; }
    }
}
