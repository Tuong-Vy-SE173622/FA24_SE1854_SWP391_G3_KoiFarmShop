namespace KoiFarmShop.Business.Dto.Payment
{
    public class PaymentResponse
    {
        public bool checkSignature { get; set; }
        public bool checkTmnCode { get; set; }
        public string ResponseCode { get; set; }
    }
}
