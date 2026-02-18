using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;
using PerfumeStore.Infrastructure.Repositories;

namespace PerfumeStore.Infrastructure.Extensions {
    public static class ServiceCollectionExtensions {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("PerfumeStore");
            services.AddDbContext<PerfumeStoreDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductCategoriesRepository, ProductCategoriesRepository>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
        }

    }
}
