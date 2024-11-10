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
    public class CareRequestResponseDto
    {
        public int RequestId { get; set; }

        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }
        public DateTime? RequestedDate { get; set; }

        public string? Status { get; set; }

        public bool? IsActive { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public List<CareRequestDetailDto> CareRequestDetailsDto { get; set; } = null!;
    }
}
