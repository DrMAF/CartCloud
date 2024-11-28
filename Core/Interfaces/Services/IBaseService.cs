using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity? GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity entity);
        TEntity Create(TEntity entity);
        void Delete(TEntity entity, bool softDelete = true);
    }
}
