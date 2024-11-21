using KoiFarmShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.OrderBusiness
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto createDto, string? currentUser);
        Task<OrderResponseDto> UpdateOrderAsync(int id, OrderUpdateDto updateDto, string? currentUser);
        Task UpdateOrderStatusAfterPaymentAsync(int orderId);
        Task<OrderResponseDto> SoftDeleteOrderAsync(int id);
        Task<bool> HardDeleteOrderAsync(int id);
        Task<OrderResponseDto> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync();
        Task<IEnumerable<OrderResponseDto>> GetAllActiveOrderByIdAsync(int customerId);
        Task<OrderStatusResponseDto> GetOrderStatusByIdAsync(int orderId);
        Task<StatsForDashBoard> GetStatsForDashBoardAsync(DateTime startDate, DateTime endDate);
    }
}
