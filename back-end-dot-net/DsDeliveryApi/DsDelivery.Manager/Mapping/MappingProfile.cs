using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;

namespace DsDelivery.Manager.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<Order, CreateOrderDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<OrderProduct, ProductDTO>().ReverseMap();
        CreateMap<Product, ReferenciaProducts>().ReverseMap();
    }
}
