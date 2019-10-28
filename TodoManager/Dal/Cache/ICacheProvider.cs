using System;
using System.Collections;

namespace TodoManager.Dal.Cache
{
    public interface ICacheProvider
    {
        T Get<T>(string key);
        void Delete(string key);
        bool KeyExists(string key);
        void Set(string key, object value);
        void Set(string key, object value, DateTime expiry);
    }
}
