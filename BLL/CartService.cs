using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class CartService : BaseService<Cart, int>, ICartService
    {
        readonly ILogger<CartService> _logger;
        readonly IUnitOfWork _cartUnitOfWork;

        public CartService(IUnitOfWork cartUnitOfWork, ILogger<CartService> logger) : base(cartUnitOfWork)
        {
            _logger = logger;
            _cartUnitOfWork = cartUnitOfWork;
        }

        public List<Cart> GetCarts()
        {
            try
            {
                var kk = _cartUnitOfWork.GetRepository<Cart, int>().GetAll();
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
