using CryptoTracker.Services;

namespace CryptoTrackerTests;

public class CryptoCapitalizationCalculatorTest
{
    [Test]
    public void CalculateMarketCap_ValidPriceAndSupply_ReturnsCorrectMarketCap()
    {
        decimal? price = 50000m;
        decimal? circulatingSupply = 1000000m;
        
        var result = CryptoCapitalizationCalculator.CalculateMarketCap(price, circulatingSupply);
        
        Assert.That(result, Is.EqualTo(50000000000m));
    }

    [Test]
    public void CalculateMarketCap_NullPrice_ReturnsNull()
    {
        decimal? price = null;
        decimal? circulatingSupply = 1000000m;
        
        var result = CryptoCapitalizationCalculator.CalculateMarketCap(price, circulatingSupply);
        
        Assert.IsNull(result);
    }

    [Test]
    public void CalculateMarketCap_NullCirculatingSupply_ReturnsNull()
    {
        decimal? price = 50000m;
        decimal? circulatingSupply = null;
        
        var result = CryptoCapitalizationCalculator.CalculateMarketCap(price, circulatingSupply);
        
        Assert.IsNull(result);
    }

    [Test]
    public void CalculateMarketCap_ZeroPriceOrSupply_ReturnsZero()
    {
        decimal? price = 0m;
        decimal? circulatingSupply = 1000000m;

        var result = CryptoCapitalizationCalculator.CalculateMarketCap(price, circulatingSupply);

        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateMarketCap_ZeroCirculatingSupply_ReturnsZero()
    {
        decimal? price = 50000m;
        decimal? circulatingSupply = 0m;

        var result = CryptoCapitalizationCalculator.CalculateMarketCap(price, circulatingSupply);

        Assert.That(result, Is.EqualTo(0m));
    }
}