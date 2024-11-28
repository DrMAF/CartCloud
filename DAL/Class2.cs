//using Application.Helpers.Auth;
//using Application.Helpers.DTO;
//using Application.Interfaces;
//using DataAccess.Data;
//using Domain.DTO.Core;
//using Domain.Entities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Query;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq.Expressions;

//namespace DataAccess.Repository;

//public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
//{
//    protected readonly DataContext _dbContext;
//    // protected readonly IMapper _mapper;
//    private readonly DbSet<T> Table;
//    private readonly string UserName = "";
//    private readonly int _branchId;
//    private readonly int _companyId;
//    private readonly string IpAddress = "";
//    private readonly string UserId = "";
//    private readonly IHttpContextAccessor _iHttpContextAccessor;
//    private readonly JWTHelper _jwtHelper;
//    public GenericRepository(DataContext dbContext, IHttpContextAccessor iHttpContextAccessor, JWTHelper jwtHelper)
//    {
//        _dbContext = dbContext;
//        Table = _dbContext.Set<T>();
//        _iHttpContextAccessor = iHttpContextAccessor;
//        _jwtHelper = jwtHelper;
//        if (iHttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
//        {
//            UserName = _jwtHelper.ReadTokenClaim(TokenClaimType.UserName);
//            //UserName = _iHttpContextAccessor?.HttpContext?.User?.Identity?.Name;
//            //UserId = _iHttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.ToString();
//            //var id = _iHttpContextAccessor.HttpContext.User?.FindFirst("Id").Value;
//            var remoteIpAddress = _iHttpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
//            IpAddress = remoteIpAddress?.MapToIPv4()?.ToString();
//            _branchId = 1;// int.Parse(_jwtHelper.ReadTokenClaim(TokenClaimType.BranchId));

//            var companyIdClaim = _jwtHelper.ReadTokenClaim(TokenClaimType.CompanyId);

//            if (int.TryParse(companyIdClaim, out int companyId))
//            {
//                // Parsing was successful; use companyId
//                _companyId = companyId;
//            }
//        }
//    }
//    public bool Add(T entity)
//    {
//        if (entity == null)
//            throw new NullReferenceException();
//        //entity.Id = Guid.NewGuid();
//        //entity.CreatedBy = UserName;
//        entity.BranchId = _branchId;
//        entity.IsActive = true;
//        entity.CreatedDate = DateTime.Now;
//        entity.CreatedBy = UserName;
//        Table.Add(entity);
//        return true;
//    }
//    public bool AddRange(IEnumerable<T> entities)
//    {
//        if (entities.Count() == 0)
//            throw new NullReferenceException();


//        foreach (var entity in entities)
//        {
//            entity.IsActive = true;
//            entity.BranchId = _branchId;
//            entity.CreatedDate = DateTime.Now;
//            entity.CreatedBy = UserName;
//        }

//        Table.AddRange(entities);
//        return true;
//    }
//    public async Task<ResponseResult> UpdateAsync(T entity)
//    {
//        try
//        {
//            if (entity == null)
//                throw new NullReferenceException();
//            var model = await FindByIdAsync(entity.Id);
//            if (model == null)
//                return new ResponseResult { IsSuccess = false, Message = "البياتات غير متوفرة" };


//            entity.ModifyByName = UserName;
//            entity.ModifyCount++;
//            entity.LastModifiedDate = DateTime.Now;
//            //_mapper.Map(entity, model);
//            return new ResponseResult { IsSuccess = true, Message = "تم تحديث البيانات بنجاح", Obj = model };
//        }
//        catch (Exception ex) { return null; }

//    }
//    public async Task<string> Detatch(T entity, int id)
//    {
//        var local = _dbContext.Set<T>()
//            .Local
//            .FirstOrDefault(entry => entry.Id == id);
//        if (local != null)
//        {
//            _dbContext.Entry(local).State = EntityState.Detached;
//        }
//        _dbContext.Entry(entity).State = EntityState.Modified;
//        return "done";
//    }
//    public bool Remove(T entity, bool IsHardDeleted = true)
//    {
//        if (IsHardDeleted)
//        {
//            Table.Remove(entity);
//            return true;
//        }
//        else
//        {
//            if (entity == null)
//                throw new NullReferenceException();

//            //entity.CancelByName = UserName;
//            entity.IsActive = false;
//            entity.CancelDate = DateTime.Now;
//            //entity.IpAddress = IpAddress;

//            return true;
//        }
//    }
//    public bool RemoveRange(List<T> entity, bool IsHardDeleted = true)
//    {
//        if (IsHardDeleted)
//        {
//            Table.RemoveRange(entity);
//            return true;
//        }
//        else
//        {
//            if (entity == null)
//                throw new NullReferenceException();

//            foreach (var item in entity)
//            {
//                item.IsActive = false;
//                item.CancelDate = DateTime.Now;
//            }
//            //entity.CancelByName = UserName;

//            //entity.IpAddress = IpAddress;

//            return true;
//        }
//    }
//    public IQueryable<T> Where(Expression<Func<T, bool>> criteria)
//    {
//        return Table.Where(x => x.IsActive).Where(criteria);
//    }
//    public List<T> ToList()
//    {
//        return Table.Where(x => x.IsActive).ToList();
//    }
//    public T FindById(int id)
//    {
//        return Table.FirstOrDefault(x => x.Id == id);
//    }
//    public T FirstOrDefault(Expression<Func<T, bool>> criteria = null)
//    {
//        if (criteria != null)
//            return Table.Where(x => x.IsActive).FirstOrDefault(criteria);
//        else
//            return Table.Where(x => x.IsActive).FirstOrDefault();
//    }
//    public T LastOrDefault(Expression<Func<T, bool>> criteria = null)
//    {
//        if (criteria != null)
//            return Table.Where(x => x.IsActive).OrderBy(x => x.Id).LastOrDefault(criteria);
//        else
//            return Table.Where(x => x.IsActive).OrderBy(x => x.Id).LastOrDefault();
//    }
//    public IQueryable<TType> Select<TType>(Expression<Func<T, TType>> select)
//    {
//        return Table.Where(x => x.IsActive).Select(select);
//    }
//    public bool Any(Expression<Func<T, bool>> criteria = null)
//    {
//        if (criteria != null)
//            return Table.Where(x => x.IsActive).Any(criteria);
//        else
//            return Table.Where(x => x.IsActive).Any();
//    }
//    public async Task<bool> AnyAsync(Expression<Func<T, bool>> criteria = null)
//    {
//        if (criteria != null)
//            return await Table.Where(x => x.IsActive /**/).AnyAsync(criteria);
//        else
//            return await Table.Where(x => x.IsActive /**/).AnyAsync();
//    }
//    public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
//    {
//        IIncludableQueryable<T, object> query = null;

//        foreach (var include in includes)
//            query = Table.Include(include);

//        return query.Where(x => x.IsActive);
//    }
//    public IQueryable<T> Skip(int count)
//    {
//        return Table.Where(x => x.IsActive).Skip(count);
//    }
//    public IQueryable<T> Take(int count)
//    {
//        return Table.Where(x => x.IsActive).Take(count);
//    }
//    public IQueryable<T> AsNoTracking()
//    {
//        return Table.Where(p => p.IsActive && p.BranchId == _branchId).AsNoTracking();
//    }
//    public double Sum(Expression<Func<T, double>> criteria)
//    {
//        return Table.Where(x => x.IsActive).Sum(criteria);
//    }
//    public int Count(Expression<Func<T, bool>> criteria = null)
//    {
//        try
//        {
//            var cnt = Table.Where(x => x.IsActive).Count(criteria);
//            return cnt;
//        }
//        catch (Exception e)
//        {
//            return 0;
//        }
//    }
//    public async Task<int> CountAsync(Expression<Func<T, bool>> criteria = null)
//    {
//        try
//        {
//            var cnt = await Table.Where(x => x.IsActive).CountAsync(criteria);
//            return cnt;
//        }
//        catch (Exception e)
//        {
//            return 0;
//        }
//    }
//    public int Max(Expression<Func<T, int>> criteria)
//    {
//        try
//        {
//            return Table.Where(x => x.IsActive).Max(criteria);
//        }
//        catch (Exception)
//        {
//            return 0;
//        }
//    }
//    public IQueryable<T> OrderBy(Expression<Func<T, int>> criteria)
//    {
//        return Table.Where(x => x.IsActive).OrderBy(criteria);
//    }
//    // Async
//    public async Task<List<T>> ToListAsync()
//    {
//        var list = await Table.Where(x => x.IsActive).ToListAsync();
//        return list;
//    }
//    public async Task<T> FindByIdAsync(int id)
//    {
//        var model = await Table.FindAsync(id);
//        if (model != null)
//        {
//            //    if (!model.IsActive)
//            //        return model;
//            return model;
//        }

//        return model;
//    }
//    public async Task<bool> AddAsync(T entity)
//    {
//        if (entity == null)
//            throw new NullReferenceException();
//        //int no;
//        //try
//        //{

//        //    no = await Table.Where(a => a.BranchId == _branchId).MaxAsync(e => e.No) + 1;
//        //}
//        //catch (Exception)
//        //{
//        //    no = 1;
//        //}
//        entity.CreatedBy = UserName;
//        entity.IsActive = true;
//        entity.CreatedDate = DateTime.Now;
//        if (entity.BranchId == 0)
//            entity.BranchId = _branchId;
//        //entity.IpAddress = IpAddress;
//        //entity.CreateById = UserId;

//        await Table.AddAsync(entity);
//        return true;
//    }
//    public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
//    {
//        if (entities.Count() == 0)
//            throw new NullReferenceException();

//        foreach (var entity in entities)
//        {
//            //entity.CreatedById = UserId;
//            entity.CreatedBy = UserName;
//            entity.IsActive = true;
//            entity.BranchId = _branchId;
//            entity.CreatedDate = DateTime.Now;
//            //entity.IpAddress = IpAddress;
//        }

//        await Table.AddRangeAsync(entities);
//        return true;
//    }
//    public async Task<ResponseResult> DeleteAsync(int id)
//    {
//        var model = await FindByIdAsync(id);
//        if (model == null)
//            return new ResponseResult { IsSuccess = false, Message = "البياتات غير متوفرة" };
//        Remove(model);
//        return new ResponseResult { IsSuccess = true, Message = "تم حذف البيانات بنجاح" };
//    }
//    //new
//    public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> criteria = null)
//    {
//        return await Table.LastOrDefaultAsync();
//    }
//    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria = null)
//    {
//        if (criteria == null)
//            return await Table.FirstOrDefaultAsync();
//        else
//            return await Table.FirstOrDefaultAsync(criteria);
//    }
//    public async Task<bool> AddRangeAsync(List<T> entitys)
//    {

//        await Table.AddRangeAsync(entitys);
//        return true;
//    }
//    public IQueryable<T> OrderByDescending(Expression<Func<T, object>> criteria)
//    {
//        return Table.OrderByDescending(criteria);

//    }
//    public async Task<int> MaxAsync(Expression<Func<T, int>> criteria)
//    {
//        var result = 0;
//        try
//        {
//            result = await Table.MaxAsync(criteria);
//        }
//        catch (Exception)
//        {
//        }
//        return result;

//    }
//    public bool DeleteRange(List<T> entity)
//    {
//        try
//        {
//            Table.RemoveRange(entity);

//            return true;
//        }
//        catch (Exception)
//        {
//            return false;
//            throw;
//        }
//    }
//    public bool DeleteRangeAsync(ICollection<T> entity)
//    {
//        try
//        {
//            Table.RemoveRange(entity);

//            return true;
//        }
//        catch (Exception)
//        {
//            return false;
//            throw;
//        }
//    }




//    public IQueryable<T> GetAllQueryable()
//    {
//        return Table.Where(x => x.IsActive && x.BranchId == _branchId);
//    }

//    public async Task<Paging<List<T>>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy)
//    {
//        IQueryable<T> query = _dbContext.Set<T>();
//        query = OrderBy(query);
//        //if (where != null) { query = query.Where(where); }
//        //if (include != null) { query = include(query); }
//        int recCount = query.Count();
//        int totalPages = (int)Math.Ceiling((decimal)recCount / (decimal)pageSize);
//        var skip = (pageNumber - 1) * pageSize;
//        var data = await query.Skip(skip).Take(pageSize).ToListAsync();
//        return new Paging<List<T>>(data, totalPages, pageNumber, pageSize, recCount);
//    }
//    public IQueryable<T> SameBranch()
//    {
//        return Table.Where(x => x.IsActive);
//    }

//    public async Task<List<T>> ToListAsyncNoFilter(Expression<Func<T, bool>> expression = null)
//    {
//        if (expression == null)
//        {
//            return await Table.Where(e => e.IsActive)/*.Where(a => a.BranchId == _branchId)*/.ToListAsync();
//        }
//        else
//        {
//            return await Table.Where(e => e.IsActive).Where(expression)/*.Where(a => a.BranchId == _branchId)*/.ToListAsync();

//        }
//        return await Table.Where(expression)/*.Where(a => a.BranchId == _branchId)*/.ToListAsync();
//        //return await Table.Where(x => x.IsActive ).ToListAsync();
//    }
//    public async Task<List<T>> ToListAsyncBasedBransh()
//    {
//        return await Table.Where(a => a.IsActive && a.BranchId == _branchId).ToListAsync();
//    }

//    public IReadOnlyList<object> GetUserBranch(string userId)
//    {
//        var res = (from br in _dbContext.Branchs
//                   join userBr in _dbContext.UserBranches on br.Id equals userBr.UserBranchId
//                   join user in _dbContext.Users on userBr.UserId equals user.Id
//                   where user.Id == userId
//                   select new { br.NameAr, br.Id, }).ToList();

//        return res;
//    }
//}
