using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.KoiTypes
{
    public class KoiTypeDto
    {
        [Key]
        public int KoiTypeId { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string OriginHistory { get; set; }

        public string CategoryDescription { get; set; }

        public string FengShui { get; set; }

        public string RaisingCondition { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
