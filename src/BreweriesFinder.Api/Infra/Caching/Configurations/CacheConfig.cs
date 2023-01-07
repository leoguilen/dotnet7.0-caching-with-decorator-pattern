namespace BreweriesFinder.Api.Infra.Caching.Configurations;

internal record CacheConfig
{
    [Required]
    public CacheProvider Provider { get; init; }

    public long? SizeLimit { get; init; }

    [Required]
    public EntryOptions EntryOptions { get; init; } = null!;
}
