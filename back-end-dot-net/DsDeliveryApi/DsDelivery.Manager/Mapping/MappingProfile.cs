using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Core.Shared.Dto.User;
using System;

namespace DsDelivery.Manager.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));

        CreateMap<Order, CreateOrderDTO>().ReverseMap();

        CreateMap<Order, UpdateOrderDTO>().ReverseMap();

        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, UpdateProductDTO>().ReverseMap();
        CreateMap<Product, CreateProductDTO>().ReverseMap();
        CreateMap<Product, ReferenciaProducts>().ReverseMap();

        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, CreateUserDTO>().ReverseMap();
        CreateMap<User, LoggedUser>().ReverseMap();
        CreateMap<UserDTO, LoggedUser>().ReverseMap();

        CreateMap<Position, PositionDTO>().ReverseMap();
        CreateMap<Position, ReferencePosition>().ReverseMap();

    }
}
