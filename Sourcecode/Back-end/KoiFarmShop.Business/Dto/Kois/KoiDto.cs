using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiFarmShop.Business.Dto.KoiTypes;

namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiDto
    {
        [Key]
        public int KoiId { get; set; }

        public int? KoiTypeId { get; set; }

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
        public KoiTypeDto? KoiType { get; set; }
    }
}
