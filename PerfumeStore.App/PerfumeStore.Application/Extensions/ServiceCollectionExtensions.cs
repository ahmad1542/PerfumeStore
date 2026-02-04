using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace PerfumeStore.Application.Extensions {
    public static class ServiceCollectionExtensions {
        public static void AddApplication(this IServiceCollection services) {

            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddAutoMapper(_ => { }, applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
        }
    }
}
