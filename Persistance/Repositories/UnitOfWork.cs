using Application.Common.Contracts.Persistance;
using Application.Common.Contracts.Repositories;
using Domain.Common.Entities;
using System.Collections;

namespace Persistence.Repositories
{
    public class UnitOfWork<TId> : IUnitOfWork<TId>
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepositoryAsync<TEntity, TId> Repository<TEntity>() where TEntity : BaseEntity<TId>
        {
            _repositories ??= new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryAsync<,>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TId)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryAsync<TEntity, TId>)_repositories[type];
        }

        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(o => o.Reload());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _context.Dispose();
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }
    }
}
