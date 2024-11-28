namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {         
        void Save();
        Task SaveAsync();
    }
}
