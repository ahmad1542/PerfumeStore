using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerfumeStore.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Extensions {
    public static class ServiceCollectionExtensions {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("PerfumeStore");
            services.AddDbContext<PerfumeStoreDbContext>(options => options.UseSqlServer(connectionString));

        }

    }
}
