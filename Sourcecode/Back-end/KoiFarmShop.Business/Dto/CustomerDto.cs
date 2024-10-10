using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto
{
    public class CustomerDto
    {
        [Key]
        public int CustomerId { get; set; }

        public int? UserId { get; set; }

        public string Address { get; set; }

        public int? LoyaltyPoints { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
        public UserDto? User { get; set; }
    }
}
