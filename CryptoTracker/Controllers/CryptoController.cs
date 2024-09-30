using CryptoTracker.Models;
using CryptoTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CryptoTracker.Controllers
{
    [Route("")]
    public class CryptoController(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : Controller
    {
        private const string BaseUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest";
        private readonly string apiKey = apiSettings.Value.ApiKey;

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("TopCrypto");
        }

        [HttpGet("topCrypto")]
        public async Task<IActionResult> TopCrypto()
        {
            try
            {
                var result = await MakeApiCall();

                if (result == null)
                {
                    return NotFound();
                }

                await CalculateTokenCapitalization(result);

                return View(result);
            }
            catch (HttpRequestException ex)
            {
                return NotFound();
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