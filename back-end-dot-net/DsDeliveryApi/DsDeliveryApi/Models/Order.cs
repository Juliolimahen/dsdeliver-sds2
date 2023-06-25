using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace DsDeliveryApi.Models
{
    public class Order
    {
        private object moment;
        public int Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Moment { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public Order(object id)
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
            double sum = 0.0;
            foreach (Product p in Products)
            {
                sum += p.Price;
            }
            return sum;
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
}