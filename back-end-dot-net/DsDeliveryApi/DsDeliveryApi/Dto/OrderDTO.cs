using System;
using System.Collections.Generic;

namespace DsDeliveryApi.Dto
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Moment { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        public List<ProductDTO> Products { get; set; }

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
