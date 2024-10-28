using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestDetailDto
    {
        public int RequestDetailId { get; set; }

        public int? Image { get; set; }

        public int? CareMethod { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }
    }
}
