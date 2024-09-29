namespace CryptoTracker.Services;

public static class CryptoCapitalizationCalculator
{
    public static decimal? CalculateMarketCap(decimal? price, decimal? circulatingSupply)
    {
        return price * circulatingSupply;
    }
}