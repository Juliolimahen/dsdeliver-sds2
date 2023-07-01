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
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<Order, CreateOrderDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, UpdateProductDTO>().ReverseMap();
        CreateMap<Order, UpdateProductDTO>().ReverseMap();
        CreateMap<Product, ReferenciaProducts>().ReverseMap();

        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, CreateUserDTO>().ReverseMap();
        CreateMap<User, LoggedUser>().ReverseMap();
        CreateMap<Position, PositionDTO>().ReverseMap();
        CreateMap<Position, ReferencePosition>().ReverseMap();

    }
}
