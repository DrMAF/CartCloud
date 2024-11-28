
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IPolygonNewsService : IBaseService<PolygonNews>
    {
        Task<List<PolygonNews>> SyncPolygonNews();
    }
}
