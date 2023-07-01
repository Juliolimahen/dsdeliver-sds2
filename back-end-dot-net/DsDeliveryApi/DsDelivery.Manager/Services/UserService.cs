using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.User;
using DsDelivery.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Manager.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwt;

        public UserService(IUserRepository repository, IMapper mapper, IJwtService jwt)
        {
            _repository = repository;
            _mapper = mapper;
            _jwt = jwt;
        }

        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await _repository.GetAsync());
        }

        public async Task<UserDTO> GetAsync(string login)
        {
            return _mapper.Map<UserDTO>(await _repository.GetAsync(login));
        }

        public async Task<UserDTO> InsertAsync(CreateUserDTO novoUser)
        {
            var usuario = _mapper.Map<User>(novoUser);
            ConverteSenhaEmHash(usuario);
            return _mapper.Map<UserDTO>(await _repository.InsertAsync(usuario));
        }

        private static void ConverteSenhaEmHash(User usuario)
        {
            var passwordHasher = new PasswordHasher<User>();
            usuario.Password = passwordHasher.HashPassword(usuario, usuario.Password);
        }

        public async Task<UserDTO> UpdateMedicoAsync(User usuario)
        {
            ConverteSenhaEmHash(usuario);
            return _mapper.Map<UserDTO>(await _repository.UpdateAsync(usuario));
        }

        public async Task<LoggedUser> ValidaUserEGeraTokenAsync(User usuario)
        {
            var usuarioConsultado = await _repository.GetAsync(usuario.Login);
            if (usuarioConsultado == null)
            {
                return null;
            }
            if (await ValidaEAtualizaHashAsync(usuario, usuarioConsultado.Password))
            {
                var usuarioLogado = _mapper.Map<LoggedUser>(usuarioConsultado);
                usuarioLogado.Token = _jwt.GerarToken(usuarioConsultado);
                return usuarioLogado;
            }
            return null;
        }

        private async Task<bool> ValidaEAtualizaHashAsync(User usuario, string hash)
        {
            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(usuario, hash, usuario.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdateMedicoAsync(usuario);
                    return true;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
