using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.StockDomain;

namespace Mizan.Infrastructure.Persistence.Configurations
{
    internal sealed class StockModelConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Provider)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.OwnsOne(x => x.BasicData, b =>
            {
                b.Property(p => p.ISINCode).HasMaxLength(50);
                b.Property(p => p.ReutersCode).HasMaxLength(50);
                b.Property(p => p.ListingDate);
                b.Property(p => p.ListedShares);
                b.Property(p => p.ParValue).HasPrecision(18, 6);
                b.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
                b.Property(p => p.SecurityType).HasMaxLength(100);
                b.Property(p => p.Sector).HasConversion<string>().HasMaxLength(100);
            });

            builder.HasMany(x => x.DailyQuotes)
                .WithOne(q => q.Stock)
                .HasForeignKey(q => q.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.IRContacts)
                .WithOne(c => c.Stock)
                .HasForeignKey(c => c.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
