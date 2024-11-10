using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiUpdateDto
    {
        public int? KoiTypeId { get; set; }

        [MaxLength(100, ErrorMessage = "Origin cannot exceed 100 characters.")]
        public string? Origin { get; set; }

        [Range(0, 1, ErrorMessage = "Gender must be 0 or 1.")]
        public int? Gender { get; set; }

        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100.")]
        public int? Age { get; set; }

        [Range(1, 100, ErrorMessage = "Size should be between 1 and 100 cm.")]
        public double? Size { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public double? Price { get; set; }
        public IFormFile? Image { get; set; }

        [MaxLength(500, ErrorMessage = "Characteristics cannot exceed 500 characters.")]
        public string? Characteristics { get; set; }

        [Range(0, 5, ErrorMessage = "Feeding amount should be between 0 and 5.")]
        public double? FeedingAmountPerDay { get; set; }

        [Range(0, 100, ErrorMessage = "Screening rate should be between 0 and 100.")]
        public double? ScreeningRate { get; set; }

        public bool? IsOwnedByFarm { get; set; }

        public bool? IsImported { get; set; }

        [MaxLength(50, ErrorMessage = "Generation cannot exceed 50 characters.")]
        public string? Generation { get; set; }

        public bool? IsLocal { get; set; }

        public bool? IsActive { get; set; }

        [MaxLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
        public string? Note { get; set; }
    }

    public class KoiStatusUpdateDto
    {
        public bool IsActive { get; set; }
    }
}
