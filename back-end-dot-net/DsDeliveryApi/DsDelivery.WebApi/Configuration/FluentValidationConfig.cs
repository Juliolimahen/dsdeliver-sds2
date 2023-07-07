using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using FluentValidation.AspNetCore;
using DsDelivery.Manager.Validator;

namespace DsDelivery.WebApi.Configuration;

public static class FluentValidationConfig
{
    public static void AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                x.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .AddJsonOptions(p => p.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .AddFluentValidation(p =>
            {
                p.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
                p.RegisterValidatorsFromAssemblyContaining<UpdateProductValidator>();
                p.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
                p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
            });

        services.AddFluentValidationRulesToSwagger();
    }
}
