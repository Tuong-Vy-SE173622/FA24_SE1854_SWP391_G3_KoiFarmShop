using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentDetailCreateDto
    {
        [Required]
        public int ConsignmentId { get; set; }

        [Required]
        public int KoiId { get; set; }

        [StringLength(30, ErrorMessage = "ConsignmentType cannot be longer than 30 characters.")]
        public string? ConsignmentType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MonthlyConsignmentFee must be a positive value.")]
        public double? MonthlyConsignmentFee { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "SoldPrice must be a positive value.")]
        public double? SoldPrice { get; set; }

        [StringLength(255, ErrorMessage = "HealthDescription cannot be longer than 255 characters.")]
        public string? HealthDescription { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive value.")]
        public double? Weight { get; set; }

        [StringLength(255, ErrorMessage = "Status cannot be longer than 255 characters.")]
        public string? Status { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(255, ErrorMessage = "Note cannot be longer than 255 characters.")]
        public string? Note { get; set; }
    }

}
