using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Mapper
{
    public class KoiTypeProfile : Profile
    {
        public KoiTypeProfile()
        {
            CreateMap<KoiTypeCreateDto, KoiType>();
            CreateMap<KoiTypeUpdateDto, KoiType>();
            CreateMap<KoiType,KoiTypeDto>();
        }
    }
}
