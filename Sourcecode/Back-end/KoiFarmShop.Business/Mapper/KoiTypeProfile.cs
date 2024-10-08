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
    public class KoiTypeProfile : Profile
    {
        public KoiTypeProfile()
        {
            CreateMap<KoiTypeCreateDto, KoiType>();
            CreateMap<KoiTypeUpdateDto, KoiType>();
            CreateMap<KoiTypeDto,KoiType>().ReverseMap();
        }
    }
}
