namespace BreweriesFinder.Api.Infra.Caching.Entries;

internal sealed class MemoryCacheEntry : ICacheEntry
{
    private readonly MemoryCacheEntryOptions _options;
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheEntry(
        IOptions<CacheConfig> cacheOptions,
        IMemoryCache memoryCache)
    {
        var entryOptions = cacheOptions.Value.EntryOptions;
        _options = new()
        {
            AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow,
            SlidingExpiration = entryOptions.SlidingExpiration,
        };
        _memoryCache = memoryCache;
    }

    public T? Get<T>(string key) => _memoryCache.Get<T?>(key);

    public T? GetOrCreate<T>(string key, Func<T> factory)
        => _memoryCache.GetOrCreate(key, entry =>
        {
            var cacheableObject = factory();
            ArgumentNullException.ThrowIfNull(cacheableObject);
            return (T?)entry
                .SetOptions(_options)
                .SetValue(cacheableObject)
                .Value;
        });

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
        => await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            var cacheableObject = await factory();
            ArgumentNullException.ThrowIfNull(cacheableObject);
            return (T?)entry
                .SetOptions(_options)
                .SetValue(cacheableObject)
                .Value;
        });

    public void Set<T>(string key, T value)
        => _memoryCache.Set(key, value, _options);
}
