
namespace Domain.Common.Entities
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
