using AutoMapper;
using KoiFarmShop.Business.Business.OrderBusiness;
using KoiFarmShop.Business.Config;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.Dto.Payment;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Business.Security;
using KoiFarmShop.Business.Utils.VNPAYAPI.Areas.VNPayAPI.Util;
using KoiFarmShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Web;

namespace KoiFarmShop.Business.Business.VNPay
{
    public class VnpayService : IVnPayService
    {
        private const string RESPONSE_CODE_SUCCESS = "00";
        private readonly VnPayConfig _vnpayConfig;
        private readonly IOrderService _orderService;

        private readonly string PREFIX = "_KOI_FARM_ORDER_";

        public VnpayService(IOptions<VnPayConfig> vnpayConfig, UnitOfWork unitOfWork, IOrderService orderService)
        {
            _vnpayConfig = vnpayConfig.Value;
            _orderService = orderService;
        }
        //TODO: save into order in database 

        // Generate Payment URL
        public string GeneratePaymentUrl(PaymentRequest request)
        {
            var pay = new PayLib();
            Random rnd = new();
            request.OrderId += PREFIX + rnd.Next(1, 1001).ToString() + "_" + rnd.Next(1, 1001).ToString();
            AddRequestData(pay, request);
            return pay.CreateRequestUrl(_vnpayConfig.Url, _vnpayConfig.HashSecret);
        }

        private void AddRequestData(PayLib pay, PaymentRequest request)
        {
            pay.AddRequestData("vnp_Version", _vnpayConfig.Version ?? "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", _vnpayConfig.TmnCode);
            pay.AddRequestData("vnp_Amount", (request.Amount * 100).ToString());
            pay.AddRequestData("vnp_BankCode", request.BankCode ?? string.Empty);
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", GetClientIpAddress());
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", request.OrderDescription);
            pay.AddRequestData("vnp_OrderType", "billpayment");
            pay.AddRequestData("vnp_ReturnUrl", _vnpayConfig.ReturnUrl);
            pay.AddRequestData("vnp_TxnRef", request.OrderId);
        }

        private string GetClientIpAddress()
        {
            string hostName = System.Net.Dns.GetHostName();
            return System.Net.Dns.GetHostAddresses(hostName).GetValue(0).ToString();
        }

        // Verify Payment Response
        public async Task<PaymentResponse?> VerifyPaymentResponseAsync(QueryString queryString)
        {
            if (!queryString.HasValue) return null;

            var json = HttpUtility.ParseQueryString(queryString.Value);

            string _orderId = json["vnp_TxnRef"]?.ToString();
            if (string.IsNullOrEmpty(_orderId)) return null;

            int orderId = int.Parse(_orderId.Split(new[] { PREFIX }, StringSplitOptions.None)[0]);
            string orderInfo = json["vnp_OrderInfo"]?.ToString();
            long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]);
            string vnp_ResponseCode = json["vnp_ResponseCode"]?.ToString();
            string vnp_SecureHash = json["vnp_SecureHash"]?.ToString();
            var pos = queryString.Value.IndexOf("&vnp_SecureHash");

            // Validate signature and TMN code
            bool isSignatureValid = ValidateSignature(queryString.Value.Substring(1, pos - 1), vnp_SecureHash, _vnpayConfig.HashSecret);
            bool isTmnCodeValid = _vnpayConfig.TmnCode == json["vnp_TmnCode"]?.ToString();

            if (isSignatureValid && isTmnCodeValid && vnp_ResponseCode.Equals(RESPONSE_CODE_SUCCESS))
            {
                //await _orderService.UpdateOrderStatusAfterPaymentAsync(orderId); it's got error since i added this line of code and idk any fuking way :D
                return new PaymentResponse()
                {
                    OrderId = orderId,
                    OrderInfo = orderInfo,
                    checkSignature = isSignatureValid,
                    checkTmnCode = isTmnCodeValid,
                    ResponseCode = vnp_ResponseCode,
                    isSuccessful = true
                };
            }

            return new PaymentResponse()
            {
                OrderId = orderId,
                OrderInfo = orderInfo,
                checkSignature = isSignatureValid,
                checkTmnCode = isTmnCodeValid,
                ResponseCode = vnp_ResponseCode,
                isSuccessful = false
            };
        }

        public bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }



    }
}
