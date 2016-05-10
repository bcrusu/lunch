namespace lunch.Repositories
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity GetByKey(TKey key);

        void Add(TEntity entity);

        void Delete(TEntity entity);
    }
}
