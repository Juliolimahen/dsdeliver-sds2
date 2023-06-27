using System.Collections.Generic;
using System;

namespace DsDelivery.Core.Shared
{
    public class CreateOrderDTO
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Moment { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        public List<ProductDTO> Products { get; set; }

        public CreateOrderDTO()
        {

        }

        public CreateOrderDTO(string address, double latitude, double longitude, DateTime moment, string status, double total, List<ProductDTO> products)
        {
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
