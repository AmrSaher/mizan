using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mizan.Domain.Repositories;
using Mizan.Infrastructure.Persistence;
using Mizan.Infrastructure.Persistence.Repositories;

namespace Mizan.IoC
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockDailyQuoteRepository, StockDailyQuoteRepository>();
            services.AddScoped<IStockIRContactRepository, StockIRContactRepository>();
            services.AddScoped<IFundRepository, FundRepository>();

            return services;
        }
    }
}
