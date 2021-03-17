using System;
using caching_componant.Configuration;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace caching_componant
{
    public class RedisCacheProvider : ICacheProvider
    {
        RedisEndpoint endPoint;
        public RedisCacheProvider(IOptions<RedisConfiguration> redisConfig)
        {
            RedisConfiguration redisConfiguration = redisConfig.Value;
            endPoint = new RedisEndpoint(redisConfiguration.Host, redisConfiguration.Port, redisConfiguration.Password, redisConfiguration.DatabaseID);
        }

        public T Get<T>(string key)
        {
            T result = default(T);

            using (RedisClient client = new RedisClient(endPoint))
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }

            return result;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public void Set<T>(string key, T value)
        {
            using (RedisClient client = new RedisClient(endPoint))
            {
                client.As<T>().SetValue(key, value);
            }
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using (RedisClient client = new RedisClient(endPoint))
            {
                client.As<T>().SetValue(key, value, timeout);
            }
        }
    }
}
