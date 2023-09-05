using DsDelivery.Data.Repositories;
using DsDelivery.Data.Repositories.Interfaces;
using DsDelivery.Data.Service;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Manager.Services;

namespace DsDelivery.WebApi.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    }
}
