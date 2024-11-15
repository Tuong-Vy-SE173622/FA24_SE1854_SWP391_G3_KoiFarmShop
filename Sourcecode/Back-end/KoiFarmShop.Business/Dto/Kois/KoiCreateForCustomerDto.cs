using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiCreateForCustomerDto
    {
        [Required]
        public int KoiTypeId { get; set; }

        [Required, MaxLength(100)]
        public string Origin { get; set; }

        [Range(0, 1, ErrorMessage = "Gender must be 0 or 1.")]
        public int? Gender { get; set; }

        [Range(0, 100)]
        public int? Age { get; set; }

        [Range(1, 200, ErrorMessage = "Size should be between 1 and 200 cm.")]
        public double? Size { get; set; }

        public IFormFile? Image { get; set; }

        public IFormFile? Certificate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public double? Price { get; set; }

        [MaxLength(500)]
        public string? Characteristics { get; set; }

        public bool? IsImported { get; set; }

        [MaxLength(50)]
        public string? Generation { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}
