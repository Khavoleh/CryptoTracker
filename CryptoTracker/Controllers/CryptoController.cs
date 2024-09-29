using CryptoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }
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