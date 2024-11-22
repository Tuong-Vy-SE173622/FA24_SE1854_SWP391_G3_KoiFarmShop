using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.ConsignmentRequest;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentFilterDto
    {
        public int? CustomerId { get; set; }
        public decimal? ArgredSalePrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public ConsignmentStatus? Status { get; set; }
        public int PageNumber { get; set; } = 1; // Default page
        public int PageSize { get; set; } = 10;
    }
}
