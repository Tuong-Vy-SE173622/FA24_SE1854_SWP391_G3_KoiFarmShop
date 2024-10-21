namespace KoiFarmShop.Business.Dto
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
