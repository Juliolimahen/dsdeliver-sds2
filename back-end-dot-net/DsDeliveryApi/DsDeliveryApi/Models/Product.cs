using System.ComponentModel.DataAnnotations.Schema;

namespace DsDeliveryApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public Product()
        {
        }

        public Product(int id, string name, double price, string description, string imageUri)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            ImageUri = imageUri;
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((Id == null) ? 0 : Id.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            Product other = (Product)obj;
            if (Id == null)
            {
                if (other.Id != null)
                    return false;
            }
            else if (!Id.Equals(other.Id))
                return false;
            return true;
        }
    }
}