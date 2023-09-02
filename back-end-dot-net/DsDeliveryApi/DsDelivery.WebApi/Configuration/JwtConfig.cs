using DsDelivery.Data.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DsDelivery.WebApi.Configuration
{
    public static class JwtConfig
    {
        public static void AddJwtTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtService, JwtService>();

            var chave = Encoding.ASCII.GetBytes(configuration.GetSection("Jwt:Secret").Value);

            services.AddAuthentication(p =>
            {
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(p =>
            {
                p.RequireHttpsMetadata = false;
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("Jwt:Audience").Value,
                    ValidateLifetime = true
                };
            });
        }

        public static void UseJwtConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
