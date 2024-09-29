using Newtonsoft.Json;

namespace CryptoTracker.Models;

public class CryptoData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    [JsonProperty("slug")]
    public string? Slug { get; set; }

    [JsonProperty("max_supply")]
    public decimal? MaxSupply { get; set; }

    [JsonProperty("circulating_supply")]
    public decimal? CirculatingSupply { get; set; }

    [JsonProperty("total_supply")]
    public decimal? TotalSupply { get; set; }

    [JsonProperty("quote")]
    public Quote Quote { get; set; } = new();
}