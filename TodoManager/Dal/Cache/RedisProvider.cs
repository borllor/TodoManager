using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace TodoManager.Dal.Cache
{
    public class RedisProdiver : IDisctributedCacheProvider
    {
        private readonly IDistributedCache cache;

        public RedisProdiver(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public void Delete(string key)
        {
            cache.Remove(key);
        }

        public Task DeleteAsync(string key)
        {
            return cache.RemoveAsync(key);
        }

        public T Get<T>(string key)
        {
            string cacheVal = cache.GetString(key);
            if (!string.IsNullOrWhiteSpace(cacheVal))
            {
                T t = JsonConvert.DeserializeObject<T>(cacheVal);
                return t;
            }

            return default(T);
        }

        public bool KeyExists(string key)
        {
            return !string.IsNullOrWhiteSpace(cache.GetString(key)) ? true : false;
        }

        public void Set(string key, object value)
        {
            if (value != null)
            {
                string cacheVal;
                if (value is string)
                {
                    cacheVal = (string)value;
                }
                else
                {
                    cacheVal = JsonConvert.SerializeObject(value);
                }
                cache.SetString(key, cacheVal);
            }
            else
            {
                cache.RemoveAsync(key);
            }
        }

        public void Set(string key, object value, DateTime expiry)
        {
            if (value != null)
            {
                DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = expiry,
                };
                string cacheVal;
                if (value is string)
                {
                    cacheVal = (string)value;
                }
                else
                {
                    cacheVal = JsonConvert.SerializeObject(value);
                }
                cache.SetString(key, cacheVal, distributedCacheEntryOptions);

            }
            else
            {
                cache.RemoveAsync(key);
            }
        }

        public Task SetAsync(string key, object value)
        {
            if (value != null)
            {
                string cacheVal;
                if (value is string)
                {
                    cacheVal = (string)value;
                }
                else
                {
                    cacheVal = JsonConvert.SerializeObject(value);
                }
                return cache.SetStringAsync(key, cacheVal);
            }
            else
            {
                return cache.RemoveAsync(key);
            }
        }

        public Task SetAsync(string key, object value, DateTime expiry)
        {
            if (value != null)
            {
                DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = expiry,
                };
                string cacheVal;
                if (value is string)
                {
                    cacheVal = (string)value;
                }
                else
                {
                    cacheVal = JsonConvert.SerializeObject(value);
                }
                return cache.SetStringAsync(key, cacheVal, distributedCacheEntryOptions);

            }
            else
            {
                return cache.RemoveAsync(key);
            }
        }
    }
}
