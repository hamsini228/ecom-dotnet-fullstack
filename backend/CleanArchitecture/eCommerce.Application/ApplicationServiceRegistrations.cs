using eCommerce.Application.Contracts;
using eCommerce.Application.JWT;
using eCommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Application;

public static class ApplicationServiceRegistrations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryService, CategoryService>();
        services.AddScoped<CustomerService, CustomerService>();
        services.AddScoped<ProductService, ProductService>();
        services.AddScoped<CartService, CartService>();
        services.AddScoped<InvoiceService, InvoiceService>();
        services.AddScoped<RoleService, RoleService>();
        services.AddScoped<CartItemService, CartItemService>();
        services.AddScoped<UserService, UserService>();
        services.AddScoped<SecurityService, SecurityService>();
        services.AddScoped<ITokenManager, TokenManager>();
        return services;
    }
}
