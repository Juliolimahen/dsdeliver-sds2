using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Core.Shared.Dto.User
{
    public class UserDTO
    {
        public string Login { get; set; }

        public ICollection<PositionDTO> Positions { get; set; }
    }
}
