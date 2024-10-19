﻿//using KoiFarmShop.Data.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace KoiFarmShop.APIService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrdersController : ControllerBase
//    {
//        private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;

//        public OrdersController(FA_SE1854_SWP391_G3_KoiFarmShopContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Orders
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
//        {
//            return await _context.Orders.ToListAsync();
//        }

//        // GET: api/Orders/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Order>> GetOrder(int id)
//        {
//            var order = await _context.Orders.FindAsync(id);

//            if (order == null)
//            {
//                return NotFound();
//            }

//            return order;
//        }

//        // PUT: api/Orders/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutOrder(int id, Order order)
//        {
//            if (id != order.OrderId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(order).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!OrderExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Orders
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Order>> PostOrder(Order order)
//        {
//            _context.Orders.Add(order);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (OrderExists(order.OrderId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
//        }

//        // DELETE: api/Orders/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOrder(int id)
//        {
//            var order = await _context.Orders.FindAsync(id);
//            if (order == null)
//            {
//                return NotFound();
//            }

//            _context.Orders.Remove(order);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool OrderExists(int id)
//        {
//            return _context.Orders.Any(e => e.OrderId == id);
//        }
//    }
//}

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
