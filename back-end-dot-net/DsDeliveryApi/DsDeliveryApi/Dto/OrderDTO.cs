using System.Collections.Generic;
using System;
using DsDeliveryApi.Models;
using System.Linq;

namespace DsDeliveryApi.Dto
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Moment { get; set; }
        public OrderStatus Status { get; set; }
        public double Total { get; set; }

        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();

        public OrderDTO()
        {
        }

        public OrderDTO(long id, string address, double latitude, double longitude, DateTime moment,
            OrderStatus status, double total)
        {
            Id = id;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            Moment = moment;
            Status = status;
            Total = total;
        }

        public OrderDTO(Order entity)
        {
            Id = entity.Id;
            Address = entity.Address;
            Latitude = entity.Latitude;
            Longitude = entity.Longitude;
            Moment = entity.Moment;
            Status = entity.Status;
            Total = entity.GetTotal();
            Products = entity.Products.Select(x => new ProductDTO(x)).ToList();
        }
    }
}