using System;
using System.Threading.Tasks;

namespace TodoManager.Dal.Cache
{
    public interface IDisctributedCacheProvider : ICacheProvider
    {
        Task DeleteAsync(string key);
        Task SetAsync(string key, object value);
        Task SetAsync(string key, object value, DateTime expiry);
    }
}
