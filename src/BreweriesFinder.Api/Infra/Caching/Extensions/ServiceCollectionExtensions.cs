namespace BreweriesFinder.Api.Infra.Caching.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraCachingServices(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .LoadConfigurations(configuration.GetSection(nameof(CacheConfig)))
            .ConfigureProviders(configuration);

    private static IServiceCollection LoadConfigurations(
        this IServiceCollection services,
        IConfigurationSection configurationSection)
        => services
            .AddOptions<CacheConfig>()
            .Configure(configurationSection.Bind)
            .ValidateOnStart()
            .Validate(config => config is not null, $"Required config section '{nameof(CacheConfig)}' is missing.")
            .ValidateDataAnnotations()
            .Services;

    private static IServiceCollection ConfigureProviders(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var cacheConfigSection = configuration.GetSection(nameof(CacheConfig));
        var cacheProvider = cacheConfigSection.GetValue<CacheProvider>("Provider");
        var cacheSizeLimit = cacheConfigSection.GetValue<long?>("SizeLimit");

        switch (cacheProvider)
        {
            case CacheProvider.InMemory:
                services
                    .AddMemoryCache(o => o.SizeLimit = cacheSizeLimit)
                    .AddSingleton<ICacheEntry, MemoryCacheEntry>();
                break;
            case CacheProvider.Distributed:
                services
                    .AddStackExchangeRedisCache(o => o.ConfigurationOptions = ConfigurationOptions.Parse(
                        configuration: configuration.GetConnectionString("Redis"),
                        ignoreUnknown: true))
                    .AddSingleton<ICacheEntry, DistributedCacheEntry>();
                break;
            default:
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(cacheProvider),
                    actualValue: cacheProvider,
                    message: "Cache provider type is not valid.");
        }

        return services;
    }
}
