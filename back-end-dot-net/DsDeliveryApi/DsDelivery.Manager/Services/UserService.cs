using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.User;
using Microsoft.AspNetCore.Identity;
using DsDelivery.Data.Service;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Data.Repositories.Interfaces;

namespace DsDelivery.Manager.Services
{
    public class UserService: IUserService
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
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await _repository.GetAllAsync());
        }

        public async Task<UserDTO> GetAsync(string login)
        {
            return _mapper.Map<UserDTO>(await _repository.GetAsync(login));
        }

        public async Task<UserDTO> InsertAsync(CreateUserDTO createUser)
        {
            var user = _mapper.Map<User>(createUser);
            ConverteSenhaEmHash(user);
            return _mapper.Map<UserDTO>(await _repository.AddAsync(user));
        }

        public void ConverteSenhaEmHash(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
        }

        public async Task<UserDTO> UpdateAsync(User user)
        {
            ConverteSenhaEmHash(user);
            return _mapper.Map<UserDTO>(await _repository.UpdateAsync(user));
        }

        public async Task<LoggedUser> ValidaUserEGeraTokenAsync(User user)
        {
            var userConsulted = await _repository.GetAsync(user.Login);
            if (userConsulted == null)
            {
                return null;
            }
            if (await ValidaEAtualizaHashAsync(user, userConsulted.Password))
            {
                var userLogado = _mapper.Map<LoggedUser>(userConsulted);
                userLogado.Token = _jwt.GenerateToken(userConsulted);
                return userLogado;
            }
            return null;
        }

        public async Task<bool> ValidaEAtualizaHashAsync(User user, string hash)
        {
            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(user, hash, user.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdateAsync(user);
                    return true;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
