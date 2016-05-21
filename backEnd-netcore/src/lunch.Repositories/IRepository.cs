namespace lunch.Repositories
{
    public interface IRepository<TEntity, TKey>
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);
    }
}
