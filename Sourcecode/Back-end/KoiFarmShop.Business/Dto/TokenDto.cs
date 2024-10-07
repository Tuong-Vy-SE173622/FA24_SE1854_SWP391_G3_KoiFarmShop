using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Dto
{
    public class TokenDto
    {
        public string AccessTokenToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiredAt { get; set; }
    }
}
