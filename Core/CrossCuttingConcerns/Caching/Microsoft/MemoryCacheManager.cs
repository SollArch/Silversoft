using System;
using System.Linq;
using System.Runtime.Caching;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    private readonly ObjectCache _cache = MemoryCache.Default;

    public object Get(string key)
    {
        return _cache.Get(key);
    }

    public void Add(string key, object value, int duration)
    {
        var policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(duration)
        };

        _cache.Add(key, value, policy);
    }

    public bool IsAdd(string key)
    {
        return _cache.Contains(key);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        var keysToRemove = _cache.Select(kvp => kvp.Key)
            .Where(key => key.Contains(pattern))
            .ToList();

        foreach (var key in keysToRemove)
        {
            _cache.Remove(key);
        }
    }
}