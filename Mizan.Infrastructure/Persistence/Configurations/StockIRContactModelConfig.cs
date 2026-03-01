using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.StockDomain;

namespace Mizan.Infrastructure.Persistence.Configurations
{
    internal sealed class StockIRContactModelConfig : IEntityTypeConfiguration<StockIRContact>
    {
        public void Configure(EntityTypeBuilder<StockIRContact> builder)
        {
            builder.ToTable("StockIRContacts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasMaxLength(200);
        }
    }
}
