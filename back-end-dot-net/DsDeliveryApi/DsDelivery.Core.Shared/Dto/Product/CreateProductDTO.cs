namespace DsDelivery.Core.Shared.Dto.Product;

public class CreateProductDTO : ICloneable
{
    /// <summary>
    /// Namo do produto.
    /// </summary>
    /// <example>Pizza Doce</example>
    public string Name { get; set; }

    /// <summary>
    /// Preço do produto.
    /// </summary>
    /// <example>65.76</example>
    public double Price { get; set; }

    /// <summary>
    /// Descrição do produto. 
    /// </summary>
    /// <example>Uma combinação mineira tradicional, 
    /// que acabou caindo no gosto do povo. 
    /// O sabor é baseado no doce da goiabada 
    /// combinado com o sabor irresistível dos queijos.</example>
    public string Description { get; set; }

    /// <summary>
    /// Url da imagem. 
    /// </summary>
    /// <example>www.exampleimagem.com.br</example>
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
