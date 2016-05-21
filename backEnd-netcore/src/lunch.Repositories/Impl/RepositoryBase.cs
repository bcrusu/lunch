using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace lunch.Repositories.Impl
{
    internal class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        public RepositoryBase(ApplicationDbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            DbContext = dbContext;
            Set = DbContext.Set<TEntity>();
        }

        protected ApplicationDbContext DbContext { get; private set; }

        protected DbSet<TEntity> Set { get; private set; }
        
        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Set.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Set.Remove(entity);
        }
    }
}
