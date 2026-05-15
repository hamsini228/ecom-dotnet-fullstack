using eCommerce.Application.Contracts;
using eCommerce.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace eCommerce.Infrastructure;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICommonRepository<Category>, CommonRepository<Category>>();
        services.AddScoped<ICommonRepository<Customer>, CommonRepository<Customer>>();
        services.AddScoped<ICommonRepository<Product>, CommonRepository<Product>>();
        services.AddScoped<ICommonRepository<Cart>, CommonRepository<Cart>>();
        services.AddScoped<ICommonRepository<Invoice>, CommonRepository<Invoice>>();
        services.AddScoped<ICommonRepository<CartItem>, CommonRepository<CartItem>>();
        services.AddScoped<ICommonRepository<Role>, CommonRepository<Role>>();
        services.AddScoped<ICommonRepository<User>, CommonRepository<User>>();
        services.AddScoped<ISecurityRepository, SecurityRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        //services.AddScoped<IC>
        return services;
    }
}
