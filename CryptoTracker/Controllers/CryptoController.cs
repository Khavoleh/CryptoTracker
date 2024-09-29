using CryptoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController(HttpClient httpClient) : ControllerBase
    {
        private static readonly string ApiKey = "0970314d-abb9-47ed-ace9-4527ee561e6a";
        private static readonly string BaseUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest";

        [HttpGet("latest")]
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
            request.Headers.Add("X-CMC_PRO_API_KEY", ApiKey);
            request.Headers.Add("Accept", "application/json");

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var cryptoResponse = JsonConvert.DeserializeObject<CryptoResponse>(responseBody);

            return cryptoResponse;
        }
    }
}