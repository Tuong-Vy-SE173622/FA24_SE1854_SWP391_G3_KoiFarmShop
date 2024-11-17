using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequestDetail;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestDetailUpdateDto
    {
        public int? KoiImage { get; set; }
        public string? ServiceDescription { get; set; }
        public CareRequestDetailStatus? Status { get; set; }
        public string? Note { get; set; }
    }
}
