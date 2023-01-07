namespace BreweriesFinder.Api.Abstractions;

public interface ICacheEntry
{
    T? Get<T>(string key);

    void Set<T>(string key, T value);

    T? GetOrCreate<T>(string key, Func<T> factory);

    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory);
}
