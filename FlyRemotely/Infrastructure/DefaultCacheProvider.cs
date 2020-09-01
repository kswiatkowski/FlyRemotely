using System;
using System.Web;
using System.Web.Caching;

namespace FlyRemotely.Infrastructure
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private Cache cache { get { return HttpContext.Current.Cache; } }
        
        public object Get(string key)
        {
            return cache[key];
        }

        public void Set(string key, object data, int cacheTime)
        {
            var expirationTime = DateTime.Now + TimeSpan.FromHours(cacheTime);
            cache.Insert(key, data, null, expirationTime, Cache.NoSlidingExpiration);
        }

        public void Invalidate(string key)
        {
            cache.Remove(key);
        }

        public bool IsSet(string key)
        {
            return (cache[key] != null);
        }
    }
}