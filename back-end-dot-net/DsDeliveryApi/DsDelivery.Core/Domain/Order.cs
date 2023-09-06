using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;

namespace DsDelivery.Core.Domain;

public class Order : Entity
{
    private object moment;
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Moment { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();

    public Order(object id)
    {
    }

    public Order()
    {

    }

    public Order(int id, string address, double latitude, double longitude, DateTime moment, OrderStatus status)
    {
        Id = id;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        Moment = moment;
        Status = status;
    }

    public Order(object id, string address, double latitude, double longitude, object moment, OrderStatus status) : this(id)
    {
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        this.moment = moment;
        Status = status;
    }

    public double GetTotal()
    {
        return OrderProducts.Sum(op => op.Product.Price);
    }

    public override int GetHashCode()
    {
        const int prime = 31;
        int result = 1;
        result = prime * result + (Id == null ? 0 : Id.GetHashCode());
        return result;
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        Order other = (Order)obj;
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