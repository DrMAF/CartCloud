using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IBaseRepository<TEntity> Repository { get; }
        void Commit();
        Task CommitAsync();
    }
}
