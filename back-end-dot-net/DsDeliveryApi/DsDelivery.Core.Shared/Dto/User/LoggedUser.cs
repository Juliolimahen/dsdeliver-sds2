using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Core.Shared.Dto.User
{
    public class LoggedUser
    {
        public string Login { get; set; }
        public ICollection<PositionDTO> Positions { get; set; }
        public string Token { get; set; }
    }
}
