using CryptoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CryptoTracker.Services;
using Newtonsoft.Json;

namespace CryptoTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : ControllerBase
    {
        private const string BaseUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest";
        private readonly string apiKey = apiSettings.Value.ApiKey;

        [HttpGet("topCrypto")]
        public async Task<IActionResult> GetLatestCryptoListing()
        {
            try
            {
                var result = await MakeApiCall();

                if (result == null)
                {
                    return NotFound();
                }

                await CalculateTokenCapitalization(result);

                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }
        }

        private static Task CalculateTokenCapitalization(CryptoResponse cryptoResponse)
        {
            foreach (var kvp in cryptoResponse.Data)
            {
                kvp.Value.Quote.Usd.Capitalization = CryptoCapitalizationCalculator.CalculateMarketCap(
                    kvp.Value.CirculatingSupply,
                    kvp.Value.Quote.Usd.Price);
            }

            return Task.CompletedTask;
        }

        private async Task<CryptoResponse?> MakeApiCall()
        {
            var url = $"{BaseUrl}?slug=bitcoin,ethereum,tether,bnb,solana";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
            request.Headers.Add("Accept", "application/json");

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var cryptoResponse = JsonConvert.DeserializeObject<CryptoResponse>(responseBody);

            return cryptoResponse;
        }
    }
}