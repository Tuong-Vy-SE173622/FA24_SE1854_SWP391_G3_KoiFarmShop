using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class KoiDto
    {
        public int KoiId { get; set; }
        public int KoiTypeId {  get; set; }

        public int Gender {  get; set; }
        public string Origin { get; set; }
        public int? Age { get; set; }
        public double? Size { get; set; }
        public string Characteristics { get; set; }
        public string Generation { get; set; }
        public bool? IsImported { get; set; }
        public bool? IsLocal { get; set; }
    }
}
