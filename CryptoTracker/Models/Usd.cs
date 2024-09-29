using Newtonsoft.Json;

namespace CryptoTracker.Models;

public class Usd
{
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("volume_24h")]
    public decimal? Volume24H { get; set; }

    [JsonProperty("percent_change_1h")]
    public decimal? PercentChange1H { get; set; }

    [JsonProperty("percent_change_24h")]
    public decimal? PercentChange24H { get; set; }

    [JsonProperty("percent_change_7d")]
    public decimal? PercentChange7D { get; set; }
}
