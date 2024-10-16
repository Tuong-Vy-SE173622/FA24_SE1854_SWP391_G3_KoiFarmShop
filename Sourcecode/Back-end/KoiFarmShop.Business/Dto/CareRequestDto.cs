using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Dto
{
    public class CareRequestDto
    {
        [Key]
        public int RequestId { get; set; }

        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }

        public DateTime? RequestedDate { get; set; }

        public string Status { get; set; }

        public bool? IsActive { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public CustomerDto Customer { get; set; }

        public KoiDto Koi { get; set; }
    }
}
