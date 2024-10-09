using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto
{
    public class OrderDto
    {
        [Key] public int order_id { get; set; }
        public int? customer_id { get; set; }
        public DateTime order_date { get; set; }
        public decimal? sub_amount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? VAT_amount { get; set; }
        public decimal? promotion_amount { get; set; }
        public decimal? total_amount { get; set; }
        public string payment_method { get; set; }
        public string payment_status { get; set; }
        public bool is_active { get; set; }
        public string? note { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime updated_at { get; set; }
        public string updated_by { get; set; }
        public CustomerDto customerDto { get; set; }
    }
}
