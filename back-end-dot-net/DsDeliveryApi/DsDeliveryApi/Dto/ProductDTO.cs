using DsDeliveryApi.Models;
using System.Collections.Generic;

namespace DsDeliveryApi.Dto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public ProductDTO()
        {
        }

        public ProductDTO(int id, string name, double price, string description, string imageUri)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            ImageUri = imageUri;
        }
    }
}
