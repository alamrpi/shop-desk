using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShopDesk.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        // Register repositories with their interfaces
        // Using Scoped lifetime, as it's the standard for DbContext-related services
        //services.AddScoped<IProductRepository, ProductRepository>();
        //services.AddScoped<ISaleRepository, SaleRepository>();
        // Add other repositories here...

        return services;
    }
}