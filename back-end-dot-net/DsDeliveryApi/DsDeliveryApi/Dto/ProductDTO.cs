using DsDeliveryApi.Models;

namespace DsDeliveryApi.Dto
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public ProductDTO()
        {
        }

        public ProductDTO(long id, string name, double price, string description, string imageUri)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            ImageUri = imageUri;
        }

        public ProductDTO(Product entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Price = entity.Price;
            Description = entity.Description;
            ImageUri = entity.ImageUri;
        }
    }
}