namespace BreweriesFinder.Api.Extensions;

internal static class EndpointExtensions
{
    internal static IEndpointRouteBuilder UseBreweriesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("breweries", async (
                [AsParameters] ListBreweriesRequest request,
                CancellationToken cancellationToken)
                => await request.Service.ListAsync(request, cancellationToken))
            .WithName("ListBreweries")
            .WithTags("Breweries")
            .WithOpenApi();

        routeBuilder
            .MapGet("breweries/{breweryId}", async (
                [AsParameters] GetSingleBreweryRequest request,
                CancellationToken cancellationToken)
                => await request.Service.GetByIdAsync(request, cancellationToken))
            .WithName("GetSingleBrewery")
            .WithTags("Breweries")
            .WithOpenApi();

        routeBuilder
            .MapGet("breweries/search", async (
                [AsParameters] SearchBreweriesRequest request,
                CancellationToken cancellationToken)
                => await request.Service.SearchAsync(request, cancellationToken))
            .WithName("SearchBreweries")
            .WithTags("Breweries")
            .WithOpenApi();

        return routeBuilder;
    }
}
