namespace BreweriesFinder.Api.Infra.ExternalServices.Services;

internal sealed class OpenBreweryClientService : IOpenBreweryClientService
{
    private readonly HttpClient _client;

    public OpenBreweryClientService(HttpClient client) => _client = client;

    public async Task<IReadOnlyList<BreweryResponse>> ListAsync(
        ListBreweriesRequest request,
        CancellationToken cancellationToken = default)
    {
        var baseRequestUri = _client.BaseAddress + "breweries";
        var requestUriWithQueryParams = QueryHelpers.AddQueryString(baseRequestUri, request.ToDictionary()!);

        var response = await _client.GetFromJsonAsync<BreweryHttpResponse[]>(requestUriWithQueryParams, cancellationToken);

        return Array.ConvertAll(response!, (input) => input.ToResponse());
    }

    public async Task<BreweryResponse> GetByIdAsync(
        GetSingleBreweryRequest request,
        CancellationToken cancellationToken = default)
    {
        var requestUri = _client.BaseAddress + $"breweries/{request.BreweryId}";
        var response = await _client.GetFromJsonAsync<BreweryHttpResponse>(requestUri, cancellationToken);

        return response.ToResponse();
    }

    public async Task<IReadOnlyList<BreweryResponse>> SearchAsync(
        SearchBreweriesRequest request,
        CancellationToken cancellationToken = default)
    {
        var baseRequestUri = _client.BaseAddress + "breweries/search";
        var requestUriWithQueryParams = QueryHelpers.AddQueryString(baseRequestUri, "query", request.Query);

        var response = await _client.GetFromJsonAsync<BreweryHttpResponse[]>(requestUriWithQueryParams, cancellationToken);

        return Array.ConvertAll(response!, (input) => input.ToResponse());
    }
}
