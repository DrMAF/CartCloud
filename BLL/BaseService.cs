using Core.Entities;
using Core.Interfaces;
using System.Linq.Expressions;

namespace BLL
{
    public class BaseService<TEntity, TPrimary> : IBaseService<TEntity, TPrimary> where TEntity : BaseEntity<TPrimary>
    {
        readonly IBaseRepository<TEntity, TPrimary> _repository;

        public BaseService(IBaseRepository<TEntity, TPrimary> repository)
        {
            _repository = repository;
        }

        public TEntity Create(TEntity entity)
        {
            return _repository.Create(entity);
        }

        public void Delete(TEntity entity, bool softDelete = true)
        {
            _repository.Delete(entity, softDelete);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public TEntity? GetById(TPrimary id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }
    }
}
