//using Domain.DTO.Core;
//using Domain.Entities;
//using System.Linq.Expressions;

//namespace Application.Interfaces;

//public interface IGenericRepository<T> where T : EntityBase
//{
//    IReadOnlyList<object> GetUserBranch(string userId);
//    bool Add(T entity);
//    bool AddRange(IEnumerable<T> entity);
//    Task<string> Detatch(T entity, int id);
//    Task<ResponseResult> UpdateAsync(T entity);
//    bool Remove(T entity, bool IsHardDeleted = false);
//    bool DeleteRangeAsync(ICollection<T> entity);
//    bool RemoveRange(List<T> entity, bool IsHardDeleted = false);
//    IQueryable<T> Where(Expression<Func<T, bool>> criteria);
//    //IQueryable<T> SameBranch();
//    List<T> ToList();
//    T FindById(int id);
//    T FirstOrDefault(Expression<Func<T, bool>> criteria = null);
//    T LastOrDefault(Expression<Func<T, bool>> criteria = null);
//    IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
//    IQueryable<TType> Select<TType>(Expression<Func<T, TType>> select);
//    bool Any(Expression<Func<T, bool>> criteria = null);
//    Task<bool> AnyAsync(Expression<Func<T, bool>> criteria = null);
//    IQueryable<T> AsNoTracking();
//    double Sum(Expression<Func<T, double>> criteria);
//    IQueryable<T> Take(int count);
//    IQueryable<T> Skip(int count);
//    int Count(Expression<Func<T, bool>> criteria = null);
//    Task<int> CountAsync(Expression<Func<T, bool>> criteria = null);


//    int Max(Expression<Func<T, int>> criteria);
//    IQueryable<T> OrderBy(Expression<Func<T, int>> criteria);
//    Task<List<T>> ToListAsync();
//    Task<T> FindByIdAsync(int id);
//    Task<bool> AddAsync(T entity);
//    Task<bool> AddRangeAsync(IEnumerable<T> entity);
//    Task<ResponseResult> DeleteAsync(int id);

//    bool DeleteRange(List<T> entity);
//    //Task<bool> RemoveRangeAsync(List<T> entity);
//    Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> criteria = null);
//    IQueryable<T> OrderByDescending(Expression<Func<T, object>> criteria);

//    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria = null);
//    Task<bool> AddRangeAsync(List<T> entitys);
//    Task<int> MaxAsync(Expression<Func<T, int>> criteria);
//    Task<Paging<List<T>>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy);
//    IQueryable<T> GetAllQueryable();
//    IQueryable<T> SameBranch();
//    Task<List<T>> ToListAsyncNoFilter(Expression<Func<T, bool>> expression = null);
//    Task<List<T>> ToListAsyncBasedBransh();
//}
