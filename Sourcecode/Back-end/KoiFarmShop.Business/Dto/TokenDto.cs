namespace KoiFarmShop.Business.Dto
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiredAt { get; set; }
    }
}
