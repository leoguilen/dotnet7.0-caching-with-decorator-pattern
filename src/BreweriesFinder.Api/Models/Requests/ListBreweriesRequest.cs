namespace BreweriesFinder.Api.Models.Requests;

public readonly record struct ListBreweriesRequest
{
    public IOpenBreweryClientService Service { get; init; }

    public string? City { get; init; }

    public string? Dist { get; init; }

    public string? Name { get; init; }

    public string? State { get; init; }

    public string? PostalCode { get; init; }

    public BreweryType? BreweryType { get; init; }

    public string? OrderBy { get; init; }

    [DefaultValue(0)]
    public int? PageNumber { get; init; }

    [DefaultValue(20)]
    public int? PageSize { get; init; }

    public IDictionary<string, string> ToDictionary()
    {
        var queryParamsDic = new Dictionary<string, string>(capacity: 4);

        queryParamsDic!.AddIfNotNull("by_city", City);
        queryParamsDic!.AddIfNotNull("by_dist", Dist);
        queryParamsDic!.AddIfNotNull("by_name", Name);
        queryParamsDic!.AddIfNotNull("by_state", State);
        queryParamsDic!.AddIfNotNull("by_postal", PostalCode);
        queryParamsDic!.AddIfNotNull("by_type", BreweryType?.ToString()?.ToLower());
        queryParamsDic!.AddIfNotNull("sort", OrderBy);
        queryParamsDic!.AddIfNotNull("page", PageNumber.ToString());
        queryParamsDic!.AddIfNotNull("per_page", PageSize.ToString());

        return queryParamsDic;
    }
}
