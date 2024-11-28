using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork<TEntity, TPrimary> : IDisposable where TEntity : BaseEntity<TPrimary>
    {
        IBaseRepository<TEntity, TPrimary> Repository { get; }
        void Commit();
        Task CommitAsync();
    }
}
