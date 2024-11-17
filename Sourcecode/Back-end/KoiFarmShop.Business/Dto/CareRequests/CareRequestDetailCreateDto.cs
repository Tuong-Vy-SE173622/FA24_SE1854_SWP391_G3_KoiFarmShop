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
    public class CareRequestDetailCreateDto
    {
        public int CareRequestId { get; set; }
        public int? KoiImage { get; set; }
        public string? ServiceDescription { get; set; }
        public string? Note { get; set; }
    }
}
