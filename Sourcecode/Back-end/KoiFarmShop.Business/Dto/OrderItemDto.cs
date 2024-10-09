using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto
{
    public class OrderItemDto
    {
        [Key]
        public int OrderItemId { get; set; }

        public int? OrderId { get; set; }

        public int? KoiId { get; set; }

        public int? Amount { get; set; }

        public double? Price { get; set; }

        public bool? IsActive { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
        public OrderDto? Order { get; set; }
        public KoiDto? Koi { get; set; }
    }
}
