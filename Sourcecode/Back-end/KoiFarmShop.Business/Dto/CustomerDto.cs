using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto
{
    public class CustomerDto
    {
        [Key]
        public int customer_id { get; set; }
        public int user_id { get; set; }
        public string address { get; set; }
        public int loyalty_points { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime updated_at { get; set; }
        public string updated_by { get; set; }
    }
}
