using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.StockDomain;

namespace Mizan.Infrastructure.Persistence.Configurations
{
    internal sealed class StockDailyQuoteModelConfig : IEntityTypeConfiguration<StockDailyQuote>
    {
        public void Configure(EntityTypeBuilder<StockDailyQuote> builder)
        {
            builder.ToTable("StockDailyQuotes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TradedVolume);
            builder.Property(x => x.CouponPaymentDate);
            builder.Property(x => x.CouponNumber);
            builder.Property(x => x.TradingDate);
            builder.Property(x => x.PriceEarningRatio).HasPrecision(18, 6);
            builder.Property(x => x.DividendYield).HasPrecision(18, 6);

            builder.OwnsOne(x => x.TradedValue, b =>
            {
                b.Property(p => p.Amount).HasPrecision(18, 6);
                b.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
            });

            builder.OwnsOne(x => x.ClosingPrice, b =>
            {
                b.Property(p => p.Amount).HasPrecision(18, 6);
                b.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
            });

            builder.OwnsOne(x => x.CashDividends, b =>
            {
                b.Property(p => p.Amount).HasPrecision(18, 6);
                b.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
            });

            builder.OwnsOne(x => x.MarketCap, b =>
            {
                b.Property(p => p.Amount).HasPrecision(18, 6);
                b.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
            });
        }
    }
}
