using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Mapper
{
    public class KoiProfile : Profile
    {
        public KoiProfile() {
            CreateMap<KoiCreateDto, Koi>();    
            CreateMap<KoiUpdateDto, Koi>();           
            CreateMap<Koi, KoiDto>();
        }
    }
}
