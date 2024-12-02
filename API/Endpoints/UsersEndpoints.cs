using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("", GetUsers);
            endpoint.MapGet("getById", GetUserById);
            endpoint.MapPost("", CreateUser);
            endpoint.MapPut("", UpdateUser);
            endpoint.MapDelete("", DeleteUser);
            endpoint.MapPost("login", login);

            return endpoint;
        }

        private static IResult GetUsers(IUserService userService, string search = "")
        {
            //pageIndex = pageIndex < 1 ? 1 : pageIndex;
            //pageSize = pageSize < 1 ? 1 : pageSize;

            var users = userService.GetPaginatedUsersAsync(search);

            return Results.Ok(users);
        }

        private static async Task<IResult> GetUserById(IUserService userService, int userId)
        {
            User user = await userService.GetUserByIdAsync(userId);

            return Results.Ok(user);
        }

        private static async Task<IResult> CreateUser(IUserService userService, [FromBody] CreateUserViewModel model)
        {
            IdentityResult res = await userService.CreateUserAsync(model);

            if (res.Succeeded)
            {
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(res);
            }
        }


        private static async Task<IResult> UpdateUser(IUserService userService, [FromBody] CreateUserViewModel model)
        {
            if (model.Id <= 0)
            {
                return Results.BadRequest("Id required");
            }

            var res = await userService.UpdateUserAsync(model);

            if (res.Succeeded)
            {
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(res);
            }
        }

        private static async Task<IResult> DeleteUser(IUserService userService, int userId)
        {
            if (userId <= 0)
            {
                return Results.BadRequest("Id required");
            }

            var res = await userService.DeleteUserAsync(userId);

            if (res.Succeeded)
            {
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(res);
            }
        }

        private static async Task<IResult> login(IUserService userService, LoginVM model)
        {
            //if (model.)
            //{
            //    return Results.BadRequest("Id required");
            //}

            var res = await userService.LoginAsync(model);

            if (res.Succeeded)
            {
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(res);
            }
        }
    }
}
