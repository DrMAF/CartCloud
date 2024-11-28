using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class CartService : BaseService<Cart, int>, ICartService
    {
        readonly ILogger<CartService> _logger;

        public CartService(IBaseRepository<Cart, int> repository, ILogger<CartService> logger) : base(repository)
        {
            _logger = logger;
        }

        public List<Cart> GetCarts()
        {
            try
            {
                return GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in CartService: {ex}");
                return null;
            }
        }

    }
}
