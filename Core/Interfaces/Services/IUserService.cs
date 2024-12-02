using Core.DTOs;
using Core.Entities;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        PaginatedResult<User> GetPaginatedUsersAsync(string search);
        Task<User> GetUserByIdAsync(int userId);
        Task<IdentityResult> CreateUserAsync(CreateUserViewModel model);
        Task<IdentityResult> UpdateUserAsync(CreateUserViewModel model);
        Task<IdentityResult> DeleteUserAsync(int userId);
        Task<LoginResultVM> LoginAsync(LoginVM model);
    }
}