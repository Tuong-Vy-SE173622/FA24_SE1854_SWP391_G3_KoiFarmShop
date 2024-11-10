using KoiFarmShop.Business.Dto.Payment;
using Microsoft.AspNetCore.Http;

namespace KoiFarmShop.Business.Business.VNPay
{
    public interface IVnPayService
    {
        string GeneratePaymentUrl(PaymentRequest request);
        Task<PaymentResponse?> VerifyPaymentResponseAsync(QueryString query);
    }
}
