
namespace Domain.Common.Entities
{
    public abstract class BaseEntity<TId> : AuditableEntity, IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
