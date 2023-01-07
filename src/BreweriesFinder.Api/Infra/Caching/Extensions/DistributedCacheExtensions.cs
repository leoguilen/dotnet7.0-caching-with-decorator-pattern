namespace BreweriesFinder.Api.Infra.Caching.Extensions;

internal static class DistributedCacheExtensions
{
    public static bool TryGetValue<TItem>(
        this IDistributedCache cache,
        string key,
        out TItem? item)
    {
        var data = cache.Get(key);
        if (data is null)
        {
            item = default;
            return false;
        }

        try
        {
            item = Deserialize<TItem>(data);
            return true;
        }
        catch (JsonException)
        {
            item = default;
            return false;
        }
    }

    public static TItem? GetOrCreate<TItem>(
        this IDistributedCache cache,
        string key,
        Func<TItem> factory,
        DistributedCacheEntryOptions options)
    {
        if (!cache.TryGetValue(key, out TItem? result))
        {
            result = factory();
            cache.Set(key, SerializeToUtf8Bytes(result), options);
        }

        return result;
    }

    public static async Task<TItem?> GetOrCreateAsync<TItem>(
        this IDistributedCache cache,
        string key,
        Func<Task<TItem>> factory,
        DistributedCacheEntryOptions options,
        CancellationToken cancellationToken = default)
    {
        if (!cache.TryGetValue(key, out TItem? result))
        {
            result = await factory();
            await cache.SetAsync(key, SerializeToUtf8Bytes(result), options, cancellationToken);
        }

        return result;
    }
}
