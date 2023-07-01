using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Core.Domain
{
    public class Position
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
