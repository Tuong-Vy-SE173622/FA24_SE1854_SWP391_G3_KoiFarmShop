using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.AutoMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Customer, CustomerDto>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)).ReverseMap();

            CreateMap<Koi, KoiDto>().ForMember(dest => dest.KoiType, opt => opt.MapFrom(src => src.KoiType)).ReverseMap();

            CreateMap<KoiType, KoiTypeDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer)).ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order)).ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<ConsignmentRequest, ConsignmentRequestDto>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer)).ReverseMap();

            CreateMap<ConsignmentDetail, ConsignmentDetailDto>().ForMember(dest => dest.Consignment, opt => opt.MapFrom(src => src.Consignment))
                .ForMember(dest => dest.Koi, opt => opt.MapFrom(src=>src.Koi))
                .ReverseMap();
        }
    }
}
