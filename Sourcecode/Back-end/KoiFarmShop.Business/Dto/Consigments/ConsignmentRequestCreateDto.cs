using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiFarmShop.Business.Dto.Consigments
{
    public class ConsignmentRequestCreateDto
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "KoiId is required.")]
        public int KoiId {  get; set; }

        [Required(ErrorMessage = "Agreed sale price is required."), Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal ArgredSalePrice { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        public string? Note { get; set; }

    }


}
