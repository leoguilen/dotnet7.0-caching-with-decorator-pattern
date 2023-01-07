namespace BreweriesFinder.Api.Infra.ExternalServices.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraExternalServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddHttpClient<IOpenBreweryClientService, OpenBreweryClientService>(client =>
            {
                var configSection = configuration.GetSection("OpenBreweryDbApi");
                ArgumentNullException.ThrowIfNull(configSection);

                client.BaseAddress = configSection.GetValue<Uri>("BaseUrl");
                client.Timeout = TimeSpan.FromMilliseconds(configSection.GetValue<int>("TimeoutInMilliseconds"));
            })
            .Services
            .Decorate<IOpenBreweryClientService, CachedOpenBreweryClientService>();
    }
}
