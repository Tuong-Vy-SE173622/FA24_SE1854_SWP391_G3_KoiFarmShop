using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiFilterDto
    {
        public string? TypeName { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public double? MinSize { get; set; }
        public double? MaxSize { get; set; }
        public bool? IsOwnedByFarm { get; set; }
        public bool? IsImport { get; set; }
        public int PageNumber { get; set; } = 1; // Default to page 1
        public int PageSize { get; set; } = 10; // Default to 10 records per page
    }
}
