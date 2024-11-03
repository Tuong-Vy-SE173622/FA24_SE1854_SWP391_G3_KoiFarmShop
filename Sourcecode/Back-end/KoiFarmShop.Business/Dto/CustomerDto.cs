using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto
{
    public class CustomerDto
    {

        public int? UserId { get; set; }

        public string Address { get; set; }
    }
}
