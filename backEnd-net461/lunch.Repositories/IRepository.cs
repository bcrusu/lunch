namespace lunch.Repositories
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity FindByKey(TKey key);

        TEntity Add(TEntity entity);

        void Delete(TEntity entity);
    }
}
