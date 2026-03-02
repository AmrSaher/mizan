using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.FundDomain;

namespace Mizan.Infrastructure.Persistence.Configurations
{
    internal sealed class FundModelConfig : IEntityTypeConfiguration<Fund>
    {
        public void Configure(EntityTypeBuilder<Fund> builder)
        {
            builder.ToTable("Funds");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(x => x.Name);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(x => x.Provider)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.OwnsOne(x => x.NAV, b =>
            {
                b.Property(p => p.Amount).HasPrecision(18, 6);
                b.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
            });

            builder.OwnsOne(x => x.RICTicker, b =>
            {
                b.Property(p => p.Value).HasMaxLength(50);
            });

            builder.OwnsOne(x => x.BBGTicker, b =>
            {
                b.Property(p => p.Value).HasMaxLength(50);
            });

            builder.OwnsOne(x => x.Liquidity, b =>
            {
                b.Property(p => p.SubscriptionFrequency).HasConversion<string>().HasMaxLength(50);
                b.Property(p => p.RedemptionFrequency).HasConversion<string>().HasMaxLength(50);
            });
        }
    }
}
