using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class KoiFilterDto
    {
        public int PageSize { get; set; } = 3; // default page size // change later
        public int PageNumber { get; set; } = 1; // default page number
        public string Origin { get; set; }
        public bool? IsImported { get; set; }
        public int? KoiTypeId { get; set; }
        public string Gender { get; set; }
    }
}
