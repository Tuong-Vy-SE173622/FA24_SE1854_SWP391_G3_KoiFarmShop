using KoiFarmShop.Business.Business.OrderBusiness;
using KoiFarmShop.Business.Business.VNPay;
using KoiFarmShop.Business.Dto.Payment;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnpayService;
        private readonly IOrderService _orderService;
        private const string REDIRECT_URL = "http://localhost:5001/dashboard/purchase";

        public PaymentController(IVnPayService vnpayService, IOrderService orderService)
        {
            _vnpayService = vnpayService;
            _orderService = orderService;
        }

        // Generate VNPAY Payment URL
        [HttpPost("create-payment")]
        public IActionResult CreatePayment([FromBody] PaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var paymentUrl = _vnpayService.GeneratePaymentUrl(request);
            return Ok(new { paymentUrl });
        }

        [HttpGet("vnpay-return")]
        public async Task<IActionResult> VnPayCallback()
        {
            if (Request.QueryString.HasValue)
            {
                var response = await _vnpayService.VerifyPaymentResponseAsync(Request.QueryString);

                if (response != null && response.isSuccessful == true)
                {
                    await _orderService.UpdateOrderStatusAfterPaymentAsync(response.OrderId);
                    return Redirect(REDIRECT_URL);
                }
                else
                    return Ok(new { message = "Payment processed failed." });
            }
            else return BadRequest("not get the response");        
        }
    }
}
