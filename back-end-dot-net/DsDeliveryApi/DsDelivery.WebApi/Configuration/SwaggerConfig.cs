using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DsDelivery.WebApi.Configuration;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DsDeliveryApi",
                Version = "v1",
                Description = "API da aplicação DsDelivery.",
                Contact = new OpenApiContact
                {
                    Name = "Julio Lima",
                    Email = "juliolima_henrique@outlook.com",
                    Url = new Uri("https://github.com/juliolimahen")
                },
                License = new OpenApiLicense
                {
                    Name = "OSD",
                    Url = new Uri("https://opensource.org/osd")
                },
                TermsOfService = new Uri("https://opensource.org/osd")
            });

            // Bearer token authentication
            OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            };

            c.AddSecurityDefinition("jwt_auth", securityDefinition);

            // Make sure swagger UI requires a Bearer token specified
            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
            {
                {securityScheme, new string[] { }},
            };

            c.AddSecurityRequirement(securityRequirements);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            xmlPath = Path.Combine(AppContext.BaseDirectory, "DsDelivery.Core.Shared.xml");
            c.IncludeXmlComments(xmlPath);

            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //try
            //{
            //    c.IncludeXmlComments(xmlPath);
            //    xmlPath = Path.Combine(AppContext.BaseDirectory, "CT.Core.Shared.xml");
            //    c.IncludeXmlComments(xmlPath);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            //c.AddFluentValidationRulesScoped();
        });
    }
    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("./swagger/v1/swagger.json", "CT v1");
        });
    }
}
