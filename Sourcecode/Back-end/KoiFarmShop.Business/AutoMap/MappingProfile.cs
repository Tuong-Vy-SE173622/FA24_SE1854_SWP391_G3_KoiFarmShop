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

            CreateMap<Koi, KoiDto>()
                .ForMember(dest => dest.KoiTypeName, opt => opt.MapFrom(src => src.KoiType.Name));
            CreateMap<KoiCreateDto, Koi>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<KoiUpdateDto, Koi>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<KoiType, KoiTypeDto>().ReverseMap();
            CreateMap<KoiTypeCreateDto, KoiType>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<KoiTypeUpdateDto, KoiType>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

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
            CreateMap<ConsignmentRequestUpdateDto, ConsignmentRequest>().ForAllMembers(
                opt => opt.Condition(
                    (src, dest, srcMember) => srcMember != null &&
                                            !(srcMember is int && (int)srcMember == 0) &&
                                            !(srcMember is double && (double)srcMember == 0)
                    )
                );

            CreateMap<ConsignmentRequest, ConsignmentRequestResponseDto>();
            //considement detail MAPPER
            CreateMap<ConsignmentDetailCreateDto, ConsignmentDetail>();
            CreateMap<ConsignmentDetailUpdateDto, ConsignmentDetail>().ForAllMembers(opt => opt.Condition(
                    (src, dest, srcMember) => srcMember != null &&
                                            !(srcMember is int && (int)srcMember == 0) &&
                                            !(srcMember is double && (double)srcMember == 0)
                    )
                );
            CreateMap<ConsignmentDetail, ConsignmentDetailResponseDto>();
        }
    }
}
