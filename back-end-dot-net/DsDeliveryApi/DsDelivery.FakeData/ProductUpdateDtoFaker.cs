using Bogus;
using DsDelivery.Core.Shared.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.FakeData
{
    public class ProductUpdateDtoFaker : Faker<UpdateProductDTO>
    {
        public ProductUpdateDtoFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker + 1);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Price, f => (double)f.Finance.Amount(1, 1000));
            RuleFor(p => p.Description, f => f.Lorem.Sentence());
            RuleFor(p => p.ImageUri, f => f.Image.PicsumUrl());
        }
    }
}
