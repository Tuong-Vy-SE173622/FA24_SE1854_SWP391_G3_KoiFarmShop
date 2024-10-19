using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestCreateDto
    {

        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }
    }
}
