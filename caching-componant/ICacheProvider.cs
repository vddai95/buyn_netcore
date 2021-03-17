using System;

namespace caching_componant
{
    public interface ICacheProvider
    {
        public void Set<T>(string key, T value);

        public void Set<T>(string key, T value, TimeSpan timeout);

        public T Get<T>(string key);

        public bool Remove(string key);

        public bool IsInCache(string key);
    }
}
