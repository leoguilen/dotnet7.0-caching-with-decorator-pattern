namespace BreweriesFinder.Api.Infra.Caching.Entries;

internal sealed class DistributedCacheEntry : ICacheEntry
{
    private readonly DistributedCacheEntryOptions _options;
    private readonly IDistributedCache _distributedCache;

    public DistributedCacheEntry(
        IOptions<CacheConfig> cacheOptions,
        IDistributedCache distributedCache)
    {
        var entryOptions = cacheOptions.Value.EntryOptions;
        _options = new()
        {
            AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow,
            SlidingExpiration = entryOptions.SlidingExpiration,
        };
        _distributedCache = distributedCache;
    }

    public T? Get<T>(string key)
    {
        _distributedCache.TryGetValue<T>(key, out var item);
        return item;
    }

    public T? GetOrCreate<T>(string key, Func<T> factory)
        => _distributedCache.GetOrCreate(key, factory, _options);

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
        => await _distributedCache.GetOrCreateAsync(key, factory, _options);

    public void Set<T>(string key, T value)
        => _distributedCache.Set(key, SerializeToUtf8Bytes(value), _options);
}
