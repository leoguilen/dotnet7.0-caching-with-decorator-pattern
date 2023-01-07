namespace BreweriesFinder.Api.Infra.ExternalServices.Models;

internal readonly record struct BreweryHttpResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("brewery_type")]
    public string BreweryType { get; init; }

    [JsonPropertyName("street")]
    public string Street { get; init; }

    [JsonPropertyName("city")]
    public string City { get; init; }

    [JsonPropertyName("state")]
    public string State { get; init; }

    [JsonPropertyName("county_province")]
    public string CountryProvince { get; init; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; init; }

    [JsonPropertyName("country")]
    public string Country { get; init; }

    [JsonPropertyName("longitude")]
    public string Longitude { get; init; }

    [JsonPropertyName("latitude")]
    public string Latitude { get; init; }

    [JsonPropertyName("phone")]
    public string Phone { get; init; }

    [JsonPropertyName("website_url")]
    public Uri WebSiteUrl { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    public BreweryResponse ToResponse() => new()
    {
        Id = Id,
        Name = Name,
        BreweryType = Enum.Parse<BreweryType>(BreweryType, ignoreCase: true),
        Street = Street,
        City = City,
        State = State,
        CountryProvince = CountryProvince,
        PostalCode = PostalCode,
        Country = Country,
        Longitude = Longitude,
        Latitude = Latitude,
        Phone = Phone,
        WebSiteUrl = WebSiteUrl,
        UpdatedAt = UpdatedAt,
        CreatedAt = CreatedAt,
    };
}