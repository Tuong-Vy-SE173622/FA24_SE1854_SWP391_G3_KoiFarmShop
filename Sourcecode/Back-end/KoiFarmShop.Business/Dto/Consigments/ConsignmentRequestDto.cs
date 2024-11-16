using KoiFarmShop.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static KoiFarmShop.Data.Models.ConsignmentRequest;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestDto
    {
        public int? ConsignmentId { get; set; }
        public int? CustomerId { get; set; }

        public int? KoiId { get; set; }

        public decimal? ArgredSalePrice { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        public ConsignmentStatus? Status { get; set; }

        public string? Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public ConsignmentTransaction? ConsignmentTransaction { get; set; }
    }

}
