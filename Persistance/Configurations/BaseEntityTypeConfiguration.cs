using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configurations
{
    public abstract class BaseEntityTypeConfiguration<TBase, TId> : IEntityTypeConfiguration<TBase>
           where TBase : BaseEntity<TId>
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            //TODO - replace with SoftDeleteColumnName global constant
            var SoftDeleteColumnName = "IsDeleted";

            builder.Property<bool>(SoftDeleteColumnName);

            builder.HasQueryFilter(m => !EF.Property<bool>(m, SoftDeleteColumnName));
        }
    }
}
