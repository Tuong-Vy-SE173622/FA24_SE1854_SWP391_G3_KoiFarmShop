using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class DeleteUserDto
    {
        [Required(ErrorMessage = "UserId is required")]
        [Display(Name = "User Id")]
        public required int UserId { get; set; }
    }
}
