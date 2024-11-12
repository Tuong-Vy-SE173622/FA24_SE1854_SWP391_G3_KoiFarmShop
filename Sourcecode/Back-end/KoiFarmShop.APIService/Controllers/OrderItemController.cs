using KoiFarmShop.Business.Business.OrderBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] List<OrderItemCreateDto> orderItemCreateDtos)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            if (orderItemCreateDtos == null || !orderItemCreateDtos.Any())
            {
                return BadRequest("No items to add.");
            }
            var result = await _orderItemService.CreateOrderItemsAsync(orderItemCreateDtos, currentUser);
            //return CreatedAtAction(nameof(GetOrderItemById), new { id = result.OrderItemId }, result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemUpdateDto orderUpdateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _orderItemService.UpdateOrderItemAsync(id, orderUpdateDto, currentUser);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var result = await _orderItemService.GetOrderItemByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItem()
        {
            var result = await _orderItemService.GetAllOrderItemAsync();
            return Ok(result);
        }
    }
}