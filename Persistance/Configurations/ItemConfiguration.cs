using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ItemConfiguration : BaseEntityTypeConfiguration<Item, int>
    {
        public override void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }

}

