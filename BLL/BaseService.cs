using Core.Entities;
using Core.Interfaces;
using System.Linq.Expressions;

namespace BLL
{
    public class BaseService<TEntity, TPrimary> : IBaseService<TEntity, TPrimary> where TEntity : BaseEntity<TPrimary>
    {
        readonly IUnitOfWork<TEntity, TPrimary> _unitOfWork;

        public BaseService(IUnitOfWork<TEntity, TPrimary> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TEntity Create(TEntity entity)
        {
            _unitOfWork.Repository.Create(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public void Delete(TEntity entity, bool softDelete = true)
        {
            _unitOfWork.Repository.Delete(entity, softDelete);
            _unitOfWork.Commit();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _unitOfWork.Repository.Get(predicate);
        }

        public TEntity? GetById(TPrimary id)
        {
            return _unitOfWork.Repository.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.Repository.GetAll();
        }

        public TEntity Update(TEntity entity)
        {
            _unitOfWork.Repository.Update(entity);
            _unitOfWork.Commit();

            return entity;
        }
    }
}
