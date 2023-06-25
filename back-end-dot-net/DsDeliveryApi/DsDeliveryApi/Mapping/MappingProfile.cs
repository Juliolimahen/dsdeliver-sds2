using AutoMapper;
using DsDeliveryApi.Dto;
using DsDeliveryApi.Models;

namespace DsDeliveryApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            // Mapeie outras entidades e DTOs conforme necessário
        }
    }
}
