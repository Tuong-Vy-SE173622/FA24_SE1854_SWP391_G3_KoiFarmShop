using KoiFarmShop.Business.Config;
using KoiFarmShop.Business.Dto.Payment;
using KoiFarmShop.Business.Utils.VNPAYAPI.Areas.VNPayAPI.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Web;

namespace KoiFarmShop.Business.Business.VNPay
{
    public class VnpayService : IVnPayService
    {
        private readonly VnPayConfig _vnpayConfig;

        public VnpayService(IOptions<VnPayConfig> vnpayConfig)
        {
            _vnpayConfig = vnpayConfig.Value;
        }
        //TODO: save into order in database 

        // Generate Payment URL
        public string GeneratePaymentUrl(PaymentRequest request)
        {
            var pay = new PayLib();
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
        public PaymentResponse? VerifyPaymentResponse(QueryString queryString)
        {
            if (queryString.HasValue)
            {
                var json = HttpUtility.ParseQueryString(queryString.Value);

                long orderId = Convert.ToInt64(json["vnp_TxnRef"]);
                string orderInfor = json["vnp_OrderInfo"].ToString();
                long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]);
                string vnp_ResponseCode = json["vnp_ResponseCode"].ToString();
                string vnp_SecureHash = json["vnp_SecureHash"].ToString();
                var pos = queryString.Value.IndexOf("&vnp_SecureHash");


                bool checkSignature = ValidateSignature(queryString.Value.Substring(1, pos - 1), vnp_SecureHash, _vnpayConfig.HashSecret); //check signature
                bool checkTmnCode = _vnpayConfig.TmnCode == json["vnp_TmnCode"].ToString();
                return new PaymentResponse()
                {
                    checkSignature = checkSignature,
                    checkTmnCode = checkTmnCode,
                    ResponseCode = vnp_ResponseCode
                };
            }
            return null;
        }

        public bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}
