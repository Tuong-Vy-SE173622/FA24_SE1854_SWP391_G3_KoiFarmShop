using KoiFarmShop.Business.Business.CarePlanBusiness;
using KoiFarmShop.Business.Business.CareRequestBusiness;
using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Business.KoiBusiness;
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
        private readonly IConsignmentRequestService _consignmentRequestService;
        private readonly ICareRequestService _careRequestService;
        private const string REDIRECT_URL = "http://localhost:5001/dashboard/purchase";
        //private const string ORDER_TYPE_CONSIGNMENT = "consignment";
        private const string ORDER_TYPE_CARE = "care-request";

        public PaymentController(IVnPayService vnpayService, IOrderService orderService, IConsignmentRequestService consignmentRequestService, ICareRequestService careRequestService)
        {
            _vnpayService = vnpayService;
            _orderService = orderService;
            _consignmentRequestService = consignmentRequestService;
            _careRequestService = careRequestService;
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
        //[HttpPost("create-payment-for-consignment")]
        //public IActionResult CreatePayment([FromForm] PaymentRequestForConsignment request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    PaymentRequest paymentRequest = new()
        //    {
        //        OrderId = request.ConsignmentId,
        //        Amount = request.Amount,
        //        OrderDescription = ORDER_TYPE_CONSIGNMENT
        //    };
        //    var paymentUrl = _vnpayService.GeneratePaymentUrl(paymentRequest);
        //    return Ok(new { paymentUrl });
        //}

        [HttpPost("create-payment-for-care-request")]
        public IActionResult CreatePayment([FromForm] PaymentRequestForCare request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PaymentRequest paymentRequest = new()
            {
                OrderId = request.Care_Request_Id,
                Amount = request.Amount,
                OrderDescription = ORDER_TYPE_CARE
            };
            var paymentUrl = _vnpayService.GeneratePaymentUrl(paymentRequest);
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
                    //if(response.OrderInfo == ORDER_TYPE_CONSIGNMENT)
                    //{
                    //    await _consignmentRequestService.UpdateConsignmentStatusAfterPaymentAsync(response.OrderId);
                    //    return Redirect(REDIRECT_URL);
                    //}
                    if (response.OrderInfo == ORDER_TYPE_CARE)
                    {
                        await _careRequestService.UpdateCareRequestStatusAfterPaymentAsync(response.OrderId);
                        return Redirect(REDIRECT_URL);
                    }

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
