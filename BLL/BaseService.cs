using Core.Entities;
using Core.Interfaces;
using System.Linq.Expressions;

namespace BLL
{
    public class BaseService<TEntity, TPrimary> : IBaseService<TEntity, TPrimary> where TEntity : BaseEntity<TPrimary>
    {
        readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TEntity Create(TEntity entity)
        {
            _unitOfWork.GetRepository<TEntity, TPrimary>().Create(entity);

            return entity;
        }

        public void Delete(TEntity entity, bool softDelete = true)
        {
            _unitOfWork.GetRepository<TEntity, TPrimary>().Delete(entity, softDelete);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _unitOfWork.GetRepository<TEntity, TPrimary>().Get(predicate);
        }

        public TEntity? GetById(TPrimary id)
        {
            return _unitOfWork.GetRepository<TEntity, TPrimary>().GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.GetRepository<TEntity, TPrimary>().GetAll();
        }

        public TEntity Update(TEntity entity)
        {
            _unitOfWork.GetRepository<TEntity, TPrimary>().Update(entity);

            return entity;
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
