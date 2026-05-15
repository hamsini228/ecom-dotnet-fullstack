using Bajaj.eCommerce.Dal;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Bajaj.eCommerce.Api.ExtensionMethods;
public static class  RepositoriesExtension
{
    public static IServiceCollection AddRepositriesServices(this IServiceCollection services)
    {
        services.AddTransient<ICommonRepository<Category>, CommonRepository<Category>>();
        services.AddTransient<ICommonRepository<Cart>, CommonRepository<Cart>>();
        services.AddTransient<ICommonRepository<CartItem>, CommonRepository<CartItem>>();
        services.AddTransient<ICommonRepository<Customer>, CommonRepository<Customer>>();
        services.AddTransient<ICommonRepository<Invoice>, CommonRepository<Invoice>>();
        services.AddTransient<ICommonRepository<Product>, CommonRepository<Product>>();
        services.AddTransient<ICartRepository,CartRepository>();

        return services;
    }
}
