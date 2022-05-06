using Application.Common.Contracts.Persistance;
using Domain.Common.Entities;

namespace Application.Common.Contracts.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IRepositoryAsync<T, TId> Repository<T>() where T : BaseEntity<TId>;
        Task<int> Commit(CancellationToken cancellationToken);
        Task Rollback();
    }
}
