using Core.Entities;
using Core.Interfaces;
using System.Linq.Expressions;

namespace BLL
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        readonly IUnitOfWork<TEntity> _unitOfWork;

        public BaseService(IUnitOfWork<TEntity> unitOfWork)
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

        public TEntity? GetById(int id)
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
