namespace Meli.Data
{
    public interface IRepository<TEntity, in TEntityId> : IDisposable where TEntity : Entity<TEntityId>
    {
        void Add(TEntity item);

        Task AddAsync(TEntity item);

        void Remove(TEntity item);

        Task RemoveAsync(TEntity item);

        TEntity Get(TEntityId id);

        Task<TEntity> GetAsync(TEntityId id);

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}