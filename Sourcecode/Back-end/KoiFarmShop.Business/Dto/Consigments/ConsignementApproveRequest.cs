using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.ConsignmentRequest;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignementApproveRequest
    {
        public int ConsignmentId { get; set; }

        public bool IsActive { get; set; }

        public ConsignmentStatus Status { get; set; }
    }
}
