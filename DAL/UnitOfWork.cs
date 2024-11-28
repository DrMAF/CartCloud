using Core.Entities;
using Core.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;

        public readonly IBaseRepository<Cart, int> CartRepository;
        public readonly IBaseRepository<PolygonNews, string> PolygonNewsRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            CartRepository = new BaseRepository<Cart, int>(_context);
            PolygonNewsRepository = new BaseRepository<PolygonNews, string>(_context);
        }

        public void Save()
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

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}