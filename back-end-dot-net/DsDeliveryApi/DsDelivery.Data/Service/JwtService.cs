using DsDelivery.Core.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DsDelivery.Data.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Secret").Value);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Login)
        };

            claims.AddRange(user.Positions.Select(p => new Claim(ClaimTypes.Role, p.Description)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration.GetSection("Jwt:Audience").Value,
                Issuer = _configuration.GetSection("Jwt:Issuer").Value,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("Jwt:ExpiraEmMinutos").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
