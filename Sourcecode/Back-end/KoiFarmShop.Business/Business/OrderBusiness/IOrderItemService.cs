using KoiFarmShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.OrderBusiness
{
    public interface IOrderItemService
    {
        Task<OrderItemResponseDto> CreateOrderItemAsync(OrderItemCreateDto createDto);
        Task<OrderItemResponseDto> UpdateOrderItemAsync(int id, OrderItemUpdateDto updateDto);
        Task<bool> DeleteOrderItemAsync(int id);
        Task<OrderItemResponseDto> GetOrderItemByIdAsync(int id);
        Task<IEnumerable<OrderItemResponseDto>> GetAllOrderItemAsync();
    }
}
