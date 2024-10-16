using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Dto
{
    public class CareRequestDetailDto
    {
        [Key]
        public int RequestDetailId { get; set; }

        public int RequestId { get; set; }

        public int? Image { get; set; }

        public int? CareMethod { get; set; }

        public string Status { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public CareRequestDto CareRequest { get; set; }
    }
}
