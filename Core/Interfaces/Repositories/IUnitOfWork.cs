using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity, TPrimary> GetRepository<TEntity, TPrimary>() where TEntity : BaseEntity<TPrimary>;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
