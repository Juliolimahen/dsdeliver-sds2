using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Core.Domain
{
    public class Customer:Entity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Sexo Sexo { get; set; }
        public ICollection<Phone> Phones { get; set; }
        public string Document { get; set; }
        public DateTime Creation { get; set; }
        public DateTime? LastUpdate { get; set; }
        public Address Address { get; set; }
    }
}
