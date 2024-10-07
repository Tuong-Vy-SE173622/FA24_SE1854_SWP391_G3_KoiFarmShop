using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.AutoMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Koi, KoiDto>().ReverseMap(); 
            CreateMap<KoiType, KoiTypeDto>().ReverseMap();
        }
    }
}
