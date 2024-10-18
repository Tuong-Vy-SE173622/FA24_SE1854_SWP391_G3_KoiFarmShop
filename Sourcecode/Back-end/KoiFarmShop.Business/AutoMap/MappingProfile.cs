using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.Dto.KoiTypes;
using KoiFarmShop.Business.Dto.Promotion;
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
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, EditUserDto>().ReverseMap();
            CreateMap<User, DeleteUserDto>().ReverseMap();
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<Promotion, PromotionCreateDto>().ReverseMap();
            CreateMap<Promotion, DeletePromotionDto>().ReverseMap();

            //consignment request MAPPER
            CreateMap<ConsignmentRequestCreateDto, ConsignmentRequest>();
            CreateMap<ConsignmentUpdateDto, ConsignmentRequest>();
            CreateMap<ConsignmentRequest, ConsignmentRequestResponseDto>();
            //considement detail MAPPER
            CreateMap<ConsignmentDetailCreateDto, ConsignmentDetail>();
            CreateMap<ConsignmentDetailUpdateDto, ConsignmentDetail>();
            CreateMap<ConsignmentDetail, ConsignmentDetailResponseDto>();
        }
    }
}
