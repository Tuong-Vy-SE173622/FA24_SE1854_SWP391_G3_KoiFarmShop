using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.KoiTypes
{
    public class KoiTypeFilterDto
    {
        public string? Name { get; set; }   
        public bool? IsActive { get; set; }
        public int PageNumber { get; set; } = 1; // Default to page 1
        public int PageSize { get; set; } = 10; // Default to 10 records per page
    }

}
