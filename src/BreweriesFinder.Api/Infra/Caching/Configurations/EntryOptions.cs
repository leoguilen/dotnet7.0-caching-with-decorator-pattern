namespace BreweriesFinder.Api.Infra.Caching.Configurations;

internal record EntryOptions
{
    public TimeSpan AbsoluteExpirationRelativeToNow { get; init; }

    public TimeSpan SlidingExpiration { get; init; }
}
