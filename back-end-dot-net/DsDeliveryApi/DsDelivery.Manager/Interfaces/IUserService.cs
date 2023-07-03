using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Manager.Interfaces
{
    public interface IUserService
    {
        void ConverteSenhaEmHash(User user);
        Task<IEnumerable<UserDTO>> GetAsync();
        Task<UserDTO> GetAsync(string login);
        Task<UserDTO> InsertAsync(CreateUserDTO userDTO);
        Task<UserDTO> UpdateMedicoAsync(User user);
        Task<bool> ValidaEAtualizaHashAsync(User user, string hash);
        Task<LoggedUser> ValidaUserEGeraTokenAsync(User user);
    }
}
