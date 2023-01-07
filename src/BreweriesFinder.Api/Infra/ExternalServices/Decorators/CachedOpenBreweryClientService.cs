namespace BreweriesFinder.Api.Infra.ExternalServices.Decorators;

internal sealed class CachedOpenBreweryClientService : IOpenBreweryClientService
{
    private readonly IOpenBreweryClientService _openBreweryClientService;
    private readonly ICacheEntry _cache;

    public CachedOpenBreweryClientService(
        IOpenBreweryClientService openBreweryClientService,
        ICacheEntry cache)
    {
        _openBreweryClientService = openBreweryClientService;
        _cache = cache;
    }

    public async Task<IReadOnlyList<BreweryResponse>> ListAsync(
        ListBreweriesRequest request,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = BuildCacheKey(request, nameof(ListAsync));

        var response = await _cache.GetOrCreateAsync(
            key: cacheKey,
            factory: () => _openBreweryClientService.ListAsync(request, cancellationToken));

        return response ?? Array.Empty<BreweryResponse>();
    }

    public async Task<BreweryResponse> GetByIdAsync(
        GetSingleBreweryRequest request,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{Environment.MachineName}:{nameof(CachedOpenBreweryClientService)}:{nameof(GetByIdAsync)}:{request.BreweryId}";

        return await _cache.GetOrCreateAsync(
            key: cacheKey,
            factory: () => _openBreweryClientService.GetByIdAsync(request, cancellationToken));
    }

    public async Task<IReadOnlyList<BreweryResponse>> SearchAsync(
        SearchBreweriesRequest request,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{Environment.MachineName}:{nameof(CachedOpenBreweryClientService)}:{nameof(SearchAsync)}:query={request.Query}";

        var response = await _cache.GetOrCreateAsync(
            key: cacheKey,
            factory: () => _openBreweryClientService.SearchAsync(request, cancellationToken));

        return response ?? Array.Empty<BreweryResponse>();
    }

    private static string BuildCacheKey(ListBreweriesRequest request, string methodName)
    {
        var defaultCacheKey = new StringBuilder($"{Environment.MachineName}:{nameof(CachedOpenBreweryClientService)}:{methodName}");

        if (request == default)
        {
            return defaultCacheKey.ToString();
        }

        var reqAsDictionary = request.ToDictionary();
        foreach (var (key, value) in reqAsDictionary)
        {
            _ = defaultCacheKey
                .Append(':')
                .Append(key)
                .Append('=')
                .Append(value);
        }

        return defaultCacheKey.ToString();
    }
}
