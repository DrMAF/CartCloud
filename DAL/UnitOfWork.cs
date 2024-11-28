using Core.Entities;
using Core.Interfaces;
using System.Collections;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;
        private Hashtable _repositories;

        //public IBaseRepository<TEntity, TPrimary> Repository { get; set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<TEntity, TPrimary> GetRepository<TEntity, TPrimary>() where TEntity : BaseEntity<TPrimary>
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TPrimary)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepository<TEntity, TPrimary>)_repositories[type];
        }

        public void SaveChanges()
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

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}