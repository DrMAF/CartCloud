
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface ICartService : IBaseService<Cart>
    {
        List<Cart> GetCarts();
    }
}
