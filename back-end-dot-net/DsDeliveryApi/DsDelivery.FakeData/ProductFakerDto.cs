﻿using Bogus;
using DsDelivery.Core.Domain;

namespace DsDelivery.FakeData
{
    public class ProductFakerDto : Faker<Product>
    {
        public ProductFakerDto()
        {
            RuleFor(p => p.Id, f => f.IndexFaker + 1);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Price, f => (double)f.Finance.Amount(1, 1000));
            RuleFor(p => p.Description, f => f.Lorem.Sentence());
            RuleFor(p => p.ImageUri, f => f.Image.PicsumUrl());
        }
    }
}