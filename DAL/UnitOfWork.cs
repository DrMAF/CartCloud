using Core.Entities;
using Core.Interfaces;

namespace DAL
{
    public class UnitOfWork<TEntity, TPrimary> : IUnitOfWork<TEntity, TPrimary> where TEntity : BaseEntity<TPrimary>
    {
        readonly AppDbContext _context;

        public IBaseRepository<TEntity, TPrimary> Repository { get; set; }

        public UnitOfWork(AppDbContext context, IBaseRepository<TEntity, TPrimary> repository)
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