using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;

namespace API.Endpoints
{
    public static class CartEndpoints
    {
        public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapPost("GetCarts", GetCarts);
            endpoint.MapPost("Create", CreateCart);

            return endpoint;
        }

        private static IResult GetCarts(/*ICartService*/ IBaseService<Cart, int> cartService)
        {
            var carts = cartService.GetAll();
            //var carts = cartService.GetCarts();

            return Results.Ok(carts);
        }

        private static IResult CreateCart(/*ICartService*/ IBaseService<Cart, int> cartService, string name)
        {
            var cart = cartService.Create(new Cart { Name = name, Description = name });
            cartService.SaveChanges();

            return Results.Ok(cart);
        }
    }
}
