namespace CryptoTracker.Models;

public class CryptoResponse
{
    public Dictionary<string, CryptoData> Data { get; set; } = new();
}