using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequest;

namespace KoiFarmShop.Business.Dto.CareRequests
{
    public class CareRequestFilterDto
    {
        public int? CustomerId { get; set; }

        public string? CarePlanName { get; set; }

        public CareRequestStatus? Status { get; set; }

        public bool IsSortedByName { get; set; } = true;
        public bool IsAscending { get; set; } = true;

        public int PageNumber { get; set; } = 1; // Default to page 1
        public int PageSize { get; set; } = 10; // Default to 10 records per page
    }
}
