
using Application.Common.Contracts.Persistance;
using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class RepositoryAsync<TEntity, TId> : IRepositoryAsync<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly ApplicationDbContext _context;

        public RepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Entities => _context.Set<TEntity>();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }
        
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _context
                .Set<TEntity>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            TEntity exist = _context.Set<TEntity>().Find(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
    }

}
