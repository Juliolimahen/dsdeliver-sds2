﻿namespace DsDelivery.Core.Shared.Dto.Product;

public class ProductDTO : ICloneable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string ImageUri { get; set; }

    public ProductDTO()
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

    public ProductDTO(int id, string name, double price, string description, string imageUri)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        ImageUri = imageUri;
    }
}
