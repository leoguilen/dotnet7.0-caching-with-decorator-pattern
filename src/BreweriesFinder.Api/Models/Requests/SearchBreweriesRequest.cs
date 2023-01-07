namespace BreweriesFinder.Api.Models.Requests;

public readonly record struct SearchBreweriesRequest
{
    public IOpenBreweryClientService Service { get; init; }

    public string Query { get; init; }

    public string? OrderBy { get; init; }

    [DefaultValue(0)]
    public int? PageNumber { get; init; }

    [DefaultValue(20)]
    public int? PageSize { get; init; }
}
