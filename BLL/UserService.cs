using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL
{
    public class UserService : IUserService
    {
        readonly UserManager<User> _userManager;
        readonly ILogger<UserService> _logger;
        readonly JWTSettings _settings;

        public UserService(UserManager<User> userManager, 
        ILogger<UserService> logger, 
        IOptions<JWTSettings> settings)
        {
            _userManager = userManager;
            _logger = logger;
            _settings = settings.Value;
        }

        public PaginatedResult<User> GetPaginatedUsersAsync(string search)
        {
            try
            {
                var users = _userManager.Users;

                if (!string.IsNullOrEmpty(search))
                {
                    users = _userManager.Users.Where(usr => usr.FirstName.Contains(search)
                    || usr.LastName.Contains(search)
                    || (!string.IsNullOrEmpty(usr.Email) && usr.Email.Contains(search))
                    || (!string.IsNullOrEmpty(usr.PhoneNumber) && usr.PhoneNumber.Contains(search)));
                }

                //int count = users.Count();
                //int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                users = users.OrderBy(usr => usr.Id);//.Skip(pageIndex - 1).Take(pageSize);

                var result = users.ToList();

                return new PaginatedResult<User>(result, 1, 10);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in GetPaginatedUsersAsync: {ex}");

                return null;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return null;
                }

                User user = await _userManager.FindByIdAsync(userId.ToString());

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in GetUserByIdAsync: {ex}");

                return null;
            }
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserViewModel model)
        {
            try
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var res = await _userManager.CreateAsync(user, model.Password);

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in CreateUserAsync: {ex}");

                return null;
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(CreateUserViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());

                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in UpdateUserAsync: {ex}");
            }

            return null;
        }

        public async Task<IdentityResult> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in DeleteUserAsync: {ex}");
            }

            return null;
        }

        public async Task<LoginResultVM> LoginAsync(LoginVM model)
        {
            JwtSecurityToken token = new JwtSecurityToken();
            string tokenStrign = string.Empty;
            bool succeeded = false;

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    //var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    //foreach (var userRole in userRoles)
                    //{
                    //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    //}

                    token = GetToken(authClaims);
                    tokenStrign = new JwtSecurityTokenHandler().WriteToken(token);
                    succeeded = true;                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in LoginAsync: {ex}");
            }

            return new LoginResultVM
            {
                Succeeded = succeeded,
                Expiration = token.ValidTo,
                Token = tokenStrign
            };
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));

            var token = new JwtSecurityToken(issuer: _settings.ValidIssuer,
                audience: _settings.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
