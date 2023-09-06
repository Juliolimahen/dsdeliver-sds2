using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Core.Shared.Dto.Product;

public class CreateProductDTO : ICloneable
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string ImageUri { get; set; }

    public CreateProductDTO()
    {

    }

    public object Clone()
    {
        var product = (ProductDTO)MemberwiseClone();
        return product;
    }

    public ProductDTO CloneTipado()
    {
        return (ProductDTO)Clone();
    }

    public CreateProductDTO(string name, double price, string description, string imageUri)
    {
        Name = name;
        Price = price;
        Description = description;
        ImageUri = imageUri;
    }
}
