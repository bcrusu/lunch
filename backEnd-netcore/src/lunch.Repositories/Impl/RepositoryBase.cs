using System;

namespace lunch.Repositories.Impl
{
    internal class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        public virtual TEntity FindByKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
