using Core.Entities;
using Core.Interfaces;

namespace DAL
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        readonly AppDbContext _context;

        public IBaseRepository<TEntity> Repository { get; set; }

        public UnitOfWork(AppDbContext context, IBaseRepository<TEntity> repository)
        {
            _context = context;
            Repository = repository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}