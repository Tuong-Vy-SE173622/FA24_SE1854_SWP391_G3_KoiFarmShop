using KoiFarmShop.Business.Business.OrderBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
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
            var result = await _orderService.CreateOrderAsync(orderCreateDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = result.OrderId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            var result = await _orderService.UpdateOrderAsync(id, orderUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _orderService.GetAllOrderAsync();
            return Ok(result);
        }
    }
}
