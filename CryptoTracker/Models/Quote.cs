using Newtonsoft.Json;

namespace CryptoTracker.Models;

public class Quote
{
    [JsonProperty("USD")]
    public Usd Usd { get; set; } = new();
}