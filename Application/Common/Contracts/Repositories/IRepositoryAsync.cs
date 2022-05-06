using Domain.Common.Entities;

namespace Application.Common.Contracts.Persistance
{
    public interface IRepositoryAsync<T, TId> where T : class, IEntity<TId>
    {
        IQueryable<T> Entities { get; }


        Task<T> GetByIdAsync(TId id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        //TODO: Rijesiti getbyId sa parametrom includes za vezne tabele

    }
}
