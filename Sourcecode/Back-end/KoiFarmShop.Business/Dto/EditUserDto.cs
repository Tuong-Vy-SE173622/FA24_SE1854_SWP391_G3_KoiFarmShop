using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class EditUserDto
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!; // Added ConfirmPassword field
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
