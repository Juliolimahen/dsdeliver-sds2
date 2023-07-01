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
        void ConverteSenhaEmHash(User usuario);
        Task<IEnumerable<UserDTO>> GetAsync();
        Task<UserDTO> GetAsync(string login);
        Task<UserDTO> InsertAsync(CreateUserDTO novoUser);
        Task<UserDTO> UpdateMedicoAsync(User usuario);
        Task<bool> ValidaEAtualizaHashAsync(User usuario, string hash);
        Task<LoggedUser> ValidaUserEGeraTokenAsync(User usuario);
    }
}
