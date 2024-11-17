using KoiFarmShop.Business.Dto.CareRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class CarePlanDto
    {
        public int? CarePlanId { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }
        public int? DurationDays { get; set; }

        public decimal? Price { get; set; }

        public bool? IsActive { get; set; }

        public List<CareRequestDto>? CareRequestDto { get; set; } = new List<CareRequestDto>();
    }

    public class CarePlanCreateDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        public int? DurationDays { get; set; }

        public decimal? Price { get; set; }
    }

    public class CarePlanUpdateDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        public int? DurationDays { get; set; }

        public decimal? Price { get; set; }
    }

    public class CarePlanResponseDto
    {
        public int? CarePlanId { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }
        public int? DurationDays { get; set; }

        public decimal? Price { get; set; }

        public bool? IsActive { get; set; }

        public List<CareRequestDto>? CareRequestDto { get; set; }
    }
}
