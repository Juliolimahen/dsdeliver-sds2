﻿using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Product;

namespace DsDelivery.FakeData
{
    public class CreateProductDtoFaker : Faker<CreateProductDTO>
    {
        public CreateProductDtoFaker()
        {
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Price, f => (double)f.Finance.Amount(1, 1000));
            RuleFor(p => p.Description, f => f.Lorem.Sentence());
            RuleFor(p => p.ImageUri, f => f.Image.PicsumUrl());
        }
    }
}
