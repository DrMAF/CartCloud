//using Application.Interfaces.Auth;
//using Application.Interfaces.SaleInvoices;
//using Domain.Entities;

//namespace Application.Interfaces;

//public interface IUnitOfWork
//{
//    IGenericRepository<T> Repository<T>() where T : EntityBase;
//    IXtra_GenericRepository<T> Xtra_GenericRepository<T>() where T : class;
//    IItemTransactionRepository ItemTransactions { get; set; }
//    IUserRepository Users { get; set; }
//    int Complete();
//    void Dispose();
//    Task<int> CompleteAsync();
//    Task<int> ExecuteSqlRawAsync(string Query);
//}
