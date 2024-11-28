//using Application.Helpers.Auth;
//using Application.Interfaces;
//using Application.Interfaces.Auth;
//using Application.Interfaces.SaleInvoices;
//using AutoMapper;
//using Core.Interfaces;
//using DataAccess.Data;
//using DataAccess.Repository.Auth;
//using DataAccess.Repository.SaleInvoices;
//using Domain.Entities;
//using Domain.Entities.Auth;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System.Collections;
//using System.IdentityModel.Tokens.Jwt;
//namespace DataAccess.Repository;

//public class UnitOfWork : IUnitOfWork
//{
//    private readonly IMapper _mapper;
//    private readonly DataContext _context;
//    private Hashtable _repositories;
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    private readonly JWTHelper _jwtHelper;
//    public IUserRepository Users { get; set; }
//    public IItemTransactionRepository ItemTransactions { get; set; }
//    public UnitOfWork(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<ApplicationUser> userManager, JWTHelper jwtHelper)
//    {
//        _mapper = mapper;
//        _context = context;
//        _httpContextAccessor = httpContextAccessor;
//        _jwtHelper = jwtHelper;
//        ItemTransactions = new ItemTransactionRepository(_httpContextAccessor, _context, _mapper);
//        _userManager = userManager;
//        Users = new UserRepository(_context, _userManager);
//    }
//    public async Task<int> Complete() => await _context.SaveChangesAsync();

//    public IGenericRepository<T> Repository<T>() where T : EntityBase
//    {
//        if (_repositories == null) _repositories = new Hashtable();

//        var type = typeof(T).Name;

//        if (!_repositories.ContainsKey(type))
//        {
//            var repositoryType = typeof(GenericRepository<>);
//            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context, _httpContextAccessor, _jwtHelper);

//            _repositories.Add(type, repositoryInstance);
//        }

//        return (IGenericRepository<T>)_repositories[type];
//    }

//    public IXtra_GenericRepository<T> Xtra_GenericRepository<T>() where T : class
//    {
//        if (_repositories == null) _repositories = new Hashtable();

//        var type = typeof(T).Name;

//        if (!_repositories.ContainsKey(type))
//        {
//            var repositoryType = typeof(Xtra_GenericRepository<>);
//            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

//            _repositories.Add(type, repositoryInstance);
//        }

//        return (IXtra_GenericRepository<T>)_repositories[type];
//    }
//    public void Dispose() =>
//        _context.Dispose();

//    public async Task<int> CompleteAsync()
//    {
//        try
//        {
//            return await _context.SaveChangesAsync();
//        }
//        catch (Exception e)
//        {
//            throw;
//        }
//    }
//    public async Task<int> ExecuteSqlRawAsync(string Query) =>
//     await _context.Database.ExecuteSqlRawAsync(Query);

//    int IUnitOfWork.Complete()
//    {
//        throw new NotImplementedException();
//    }
//}