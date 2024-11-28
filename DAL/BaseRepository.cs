using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class BaseRepository<TEntity, TPrimary> : IBaseRepository<TEntity, TPrimary> where TEntity : BaseEntity<TPrimary>
    {
        public AppDbContext _context { get; set; }

        protected DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public TEntity? GetById(TPrimary id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            //_context.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity, bool softDelete = true)
        {
            if (softDelete)
            {
                entity.IsDeleted = true;

                Update(entity);
            }
            else
            {
                _context.Attach(entity);
                _context.Remove(entity);

                //_context.SaveChanges();
            }
        }

        public void Delete(TPrimary id, bool softDelete = true)
        {
            var entity = _dbSet.Find(id);

            if (entity != null)
            {
                if (softDelete)
                {
                    entity.IsDeleted = true;

                    Update(entity);
                }
                else
                {
                    _context.Attach(entity);
                    _context.Remove(entity);

                    //_context.SaveChanges();
                }
            }
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> predicate, bool softDelete = true)
        {
            var entities = Get(predicate);

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    if (softDelete)
                    {
                        entity.IsDeleted = true;

                        Update(entity);
                    }
                    else
                    {
                        _context.Attach(entity);
                        _context.Remove(entity);

                        //_context.SaveChanges();
                    }
                }
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;

            //_context.SaveChanges();

            return entity;
        }

    }
}
