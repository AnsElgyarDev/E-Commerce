using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ECommerceApp.Helpers;
public static class CacheExtensions
{
    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan? absoluteExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(10)
        };

        var jsonData = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, jsonData, options);
    }

    public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key)
    {
        var jsonData = await cache.GetStringAsync(key);
        if (string.IsNullOrEmpty(jsonData))
            return default;

        return JsonSerializer.Deserialize<T>(jsonData);
    }
}