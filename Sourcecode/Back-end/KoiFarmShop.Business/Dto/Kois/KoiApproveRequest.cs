using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiApproveRequest
    {
        [Required]
        public int KoiId {  get; set; }
        [Required]
        public bool IsApproved { get; set; }
    }
}
