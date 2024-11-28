using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class CartService : BaseService<Cart>, ICartService
    {
        readonly ILogger<CartService> _logger;
        readonly IUnitOfWork<Cart> _cartUnitOfWork;

        public CartService(IUnitOfWork<Cart> cartUnitOfWork, ILogger<CartService> logger) : base(cartUnitOfWork)
        {
            _logger = logger;
            _cartUnitOfWork = cartUnitOfWork;
        }

        public List<Cart> GetCarts()
        {
            try
            {
                return _cartUnitOfWork.Repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in CartService: {ex}");
                return null;
            }
        }
    }
}
