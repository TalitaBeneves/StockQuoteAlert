using System.Text.Json;
using static StockQuoteAlert.QuoteResult;

namespace StockQuoteAlert
{
    public class QuoteService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string Token = "5NnutG1JSfpN3zFRUahCqc";

        public async Task<Quote> GetQuote(string stock)
        {
            string url = $"https://brapi.dev/api/quote/{stock}?token={Token}";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            
            try
            {
                return JsonSerializer.Deserialize<Quote>(responseBody)!;
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing Json: " + ex.Message);
            }

        }
    }
}
