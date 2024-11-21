using KoiFarmShop.Business.Business.OrderBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _orderService.CreateOrderAsync(orderCreateDto, currentUser);
            return CreatedAtAction(nameof(GetOrderById), new { id = result.OrderId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _orderService.UpdateOrderAsync(id, orderUpdateDto, currentUser);
            return Ok(result);
        }

        [HttpPut("{id}/update-order-status-after-payment")]
        public async Task<IActionResult> UpdateOrderStatus(int id)
        {
            await _orderService.UpdateOrderStatusAfterPaymentAsync(id);
            return Ok(new { message = "Update Order successfully!" });
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> SoftDeleteOrder(int id)
        {
            var result = await _orderService.SoftDeleteOrderAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDeleteOrder(int id)
        {
            await _orderService.HardDeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _orderService.GetAllOrderAsync();
            return Ok(result);
        }

        [HttpGet("customer/{id}/active")]
        public async Task<IActionResult> GetAllActiveOrderById(int customerId)
        {
            var result = await _orderService.GetAllActiveOrderByIdAsync(customerId);
            return Ok(result);
        }
        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetOrderStatusById(int orderId)
        {
            var result = await _orderService.GetOrderStatusByIdAsync(orderId);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("dashboard-stats")]
        public async Task<IActionResult> GetStatsForDashBoardAsync(DateTime startDate, DateTime endDate)
        {
            var stats = await _orderService.GetStatsForDashBoardAsync(startDate, endDate);
            return Ok(stats);
        }

    }
}
