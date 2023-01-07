namespace BreweriesFinder.Api.Models.Responses;

public readonly record struct BreweryResponse
{
    public string Id { get; init; }

    public string Name { get; init; }

    public BreweryType BreweryType { get; init; }

    public string Street { get; init; }

    public string City { get; init; }

    public string State { get; init; }

    public string CountryProvince { get; init; }

    public string PostalCode { get; init; }

    public string Country { get; init; }

    public string Longitude { get; init; }

    public string Latitude { get; init; }

    public string Phone { get; init; }

    public Uri WebSiteUrl { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}
