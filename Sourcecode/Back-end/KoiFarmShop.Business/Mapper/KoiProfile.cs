using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
