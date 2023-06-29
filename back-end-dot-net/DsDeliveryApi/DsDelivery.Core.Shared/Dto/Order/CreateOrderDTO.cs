using DsDelivery.Core.Shared.Dto.Product;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DsDelivery.Core.Shared.Dto.Order;

public class CreateOrderDTO 
{
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<ReferenciaProducts> Products { get; set; }

}
