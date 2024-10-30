using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.Business.Dto
{
    public class DeleteUserDto
    {
        [Required(ErrorMessage = "UserId is required")]
        [Display(Name = "User Id")]
        public required int UserId { get; set; }
    }
}
