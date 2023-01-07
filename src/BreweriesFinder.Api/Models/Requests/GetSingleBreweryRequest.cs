namespace BreweriesFinder.Api.Models.Requests;

public readonly record struct GetSingleBreweryRequest
{
    public IOpenBreweryClientService Service { get; init; }

    public string BreweryId { get; init; }
}
