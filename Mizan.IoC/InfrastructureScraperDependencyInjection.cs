using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mizan.Application.Interfaces;
using Mizan.Infrastructure.Scraper.Services;
using Hangfire;

namespace Mizan.IoC
{
    public static class InfrastructureScraperDependencyInjection
    {
        public static IServiceCollection AddInfrastructureScraper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBackgroundJobService, HangfireService>();

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfireServer();

            return services;
        }
    }
}
