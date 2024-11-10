namespace KoiFarmShop.Business.Dto.Payment
{
    public class PaymentResponse
    {
        public long OrderId { get; set; }
        public string OrderInfo { get; set; }
        public bool checkSignature { get; set; }
        public bool checkTmnCode { get; set; }
        public string ResponseCode { get; set; }
        public bool isSuccessful { get; set; }
    }
}
