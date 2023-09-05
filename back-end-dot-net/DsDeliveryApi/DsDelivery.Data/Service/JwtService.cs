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

namespace DsDelivery.Data.Service;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;

    public JwtService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GerarToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var chave = Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value);
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login)
            };
        claims.AddRange(user.Positions.Select(p => new Claim(ClaimTypes.Role, p.Description)));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Audience = configuration.GetSection("JWT:Audience").Value,
            Issuer = configuration.GetSection("JWT:Issuer").Value,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration.GetSection("JWT:ExpiraEmMinutos").Value)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
