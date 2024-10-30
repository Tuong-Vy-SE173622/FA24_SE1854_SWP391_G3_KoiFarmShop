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

        public PaymentController(IVnPayService vnpayService)
        {
            _vnpayService = vnpayService;
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
        public IActionResult VnpayReturn()
        {
            if (Request.QueryString.HasValue)
            {
                var response = _vnpayService.VerifyPaymentResponse(Request.QueryString);
                return Ok(response);
            }
            return NoContent();
        }
    }
}
