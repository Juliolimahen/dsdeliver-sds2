using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Core.Domain
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<Position> Positions { get; set; }

        public User()
        {
            Positions = new HashSet<Position>();
        }
    }
}
