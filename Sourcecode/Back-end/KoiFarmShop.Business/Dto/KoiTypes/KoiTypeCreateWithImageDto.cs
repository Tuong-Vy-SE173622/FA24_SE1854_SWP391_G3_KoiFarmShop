using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.KoiTypes
{
    public class KoiTypeCreateWithImageDto
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string OriginHistory { get; set; }
        public string CategoryDescription { get; set; }
        public string FengShui { get; set; }
        public string RaisingCondition { get; set; }
        public string Note { get; set; }
        public IFormFile Image { get; set; }
    }
}
