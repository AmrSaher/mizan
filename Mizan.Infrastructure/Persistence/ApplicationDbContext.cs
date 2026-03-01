using Microsoft.EntityFrameworkCore;
using Mizan.Domain.Entities.FundDomain;
using Mizan.Domain.Entities.StockDomain;

namespace Mizan.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockDailyQuote> StockDailyQuotes { get; set; }
        public DbSet<StockIRContact> StockIRContacts { get; set; }
        public DbSet<Fund> Funds { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
