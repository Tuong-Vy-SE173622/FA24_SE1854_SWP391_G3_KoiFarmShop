using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.KoiTypes
{
    public class KoiTypeCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Short Description cannot exceed 250 characters.")]
        public string? ShortDescription { get; set; }

        [MaxLength(500, ErrorMessage = "Origin History cannot exceed 500 characters.")]
        public string? OriginHistory { get; set; }

        [MaxLength(500, ErrorMessage = "Category Description cannot exceed 500 characters.")]
        public string? CategoryDescription { get; set; }

        [MaxLength(500, ErrorMessage = "Feng Shui cannot exceed 500 characters.")]
        public string? FengShui { get; set; }

        [MaxLength(500, ErrorMessage = "Raising Condition cannot exceed 500 characters.")]
        public string? RaisingCondition { get; set; }

        [MaxLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
        public string? Note { get; set; }

        public IFormFile? Image { get; set; }
    }
}
