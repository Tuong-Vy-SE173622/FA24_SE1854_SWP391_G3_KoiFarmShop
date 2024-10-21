namespace KoiFarmShop.Business.Dto.Kois
{
    public class KoiCreateDto
    {
        public string Origin { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public double? Size { get; set; }
        public string Characteristics { get; set; }
        public double? FeedingAmountPerDay { get; set; }
        public double? ScreeningRate { get; set; }
        public bool? IsOwnedByFarm { get; set; }
        public bool? IsImported { get; set; }
        public string Generation { get; set; }
        public bool? IsLocal { get; set; }
        public string Note { get; set; }
    }
}
