using Application.Common.Constants;
using Application.Common.Interfaces;
using Domain.Common.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime, ICurrentUser currentUser): base(options)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
        }

        public DbSet<Item> Items => Set<Item>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser?.UserId ?? SharedConstants.NotAuthorized;
                        entry.Entity.CreatedAt = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUser?.UserId ?? SharedConstants.NotAuthorized;
                        entry.Entity.LastModifiedAt = _dateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }

}
