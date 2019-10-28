using System.Threading.Tasks;
using TodoManager.Framework.Query;

namespace TodoManager.Bus
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}