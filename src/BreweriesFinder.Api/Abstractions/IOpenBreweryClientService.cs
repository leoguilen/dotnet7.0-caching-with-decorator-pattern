namespace BreweriesFinder.Api.Abstractions;

public interface IOpenBreweryClientService
{
    Task<IReadOnlyList<BreweryResponse>> ListAsync(
        ListBreweriesRequest request,
        CancellationToken cancellationToken = default);

    Task<BreweryResponse> GetByIdAsync(
        GetSingleBreweryRequest request,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BreweryResponse>> SearchAsync(
        SearchBreweriesRequest request,
        CancellationToken cancellationToken = default);
}
