using KoiFarmShop.Business.Dto.KoiTypes;
using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiDto
    {
        [Key]
        public int KoiId { get; set; }

        public string? KoiTypeName { get; set; }

        public string? Origin { get; set; }

        public int? Gender { get; set; }

        public int? Age { get; set; }

        public double? Size { get; set; }

        public string Characteristics { get; set; }

        public double? FeedingAmountPerDay { get; set; }

        public double? ScreeningRate { get; set; }

        public bool? IsOwnedByFarm { get; set; }

        public bool? IsImported { get; set; }

        public string Generation { get; set; }

        public bool? IsLocal { get; set; }

        public bool? IsActive { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
