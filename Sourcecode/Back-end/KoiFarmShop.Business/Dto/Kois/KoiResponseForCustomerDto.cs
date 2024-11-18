using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiResponseForCustomerDto
    {

        public int KoiTypeId { get; set; }

        public string Origin { get; set; }

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
