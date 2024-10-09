using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business
{
    public interface IBusinessResult
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }
    }
}
