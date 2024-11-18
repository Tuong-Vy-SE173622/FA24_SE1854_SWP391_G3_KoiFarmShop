using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequestDetail;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestDetailResponseDto
    {
        public int CareRequestDetailId { get; set; }
        public int CareRequestId { get; set; }
        public int? KoiImage { get; set; }
        public string? ServiceDescription { get; set; }
        public CareRequestDetailStatus? Status { get; set; }
        public string? Note { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public CareRequestDto CareRequest { get; set; } = null!;

    }
}
