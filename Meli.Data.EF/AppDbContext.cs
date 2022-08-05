using Meli.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meli.Data.EF
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Returns a IDbSet instance for access to entities of the given type in the context,
        ///     the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
    }

    public class AppDbContext : DbContext, IEfUnitOfWork
    {
        #region Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        public DbSet<DNAEntity> Dna { get; set; }


        #endregion

        #region IUnitOfWork Implementation

        public void CommitChanges()
        {
            SaveChanges();
        }

        public void RollbackChanges()
        {
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        #endregion
    }
}
