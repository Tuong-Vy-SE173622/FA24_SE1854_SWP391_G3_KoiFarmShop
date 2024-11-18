using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentTransactionDto
    {
        public int TransactionId { get; set; }

        public int? ConsignmentId { get; set; }

        public decimal? SalePrice { get; set; }
        
        public decimal? CommissionFee { get; set; } = 0.05m;

        public decimal? CommissionAmount { get; set; }

        public decimal? Earnings { get; set; }

        public DateTime? SoldAt {get; set; }
    }
}
