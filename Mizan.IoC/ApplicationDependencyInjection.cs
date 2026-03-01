using Microsoft.Extensions.DependencyInjection;
using Mizan.Application.Services;

namespace Mizan.IoC
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<FundService>();

            return services;
        }
    }
}
