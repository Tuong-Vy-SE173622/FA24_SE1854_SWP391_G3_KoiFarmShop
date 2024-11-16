using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestUpdateDto
    {
        public decimal? ArgredSalePrice { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Note { get; set; }
    }

}
