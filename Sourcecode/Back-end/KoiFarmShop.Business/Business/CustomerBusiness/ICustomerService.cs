using KoiFarmShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.CustomerBusiness
{
    public interface ICustomerService
    {
        Task<IEnumerable<ListCustomerDto>> GetAllCustomersAsync();
        Task<ResultDto> GetCustomerList(int? customerId, int? userId);
    }
}
