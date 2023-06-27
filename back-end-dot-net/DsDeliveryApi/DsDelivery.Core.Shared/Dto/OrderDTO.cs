using System;
using System.Collections.Generic;

namespace DsDelivery.Core.Shared
{
    public class OrderDTO : CreateOrderDTO
    {
        public int? Id { get; set; }

        public OrderDTO()
        {
            Products = new List<ProductDTO>();
        }

        public OrderDTO(int id, string address, double latitude, double longitude, DateTime moment, string status, double total, List<ProductDTO> products)
        {
            Id = id;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            Moment = moment;
            Status = status;
            Total = total;
            Products = products;
        }
    }
}
