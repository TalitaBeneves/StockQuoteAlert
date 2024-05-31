using System.Text.Json.Serialization;

namespace StockQuoteAlert
{
    public class QuoteResult
    {
        public class Quote
        {
            [JsonPropertyName("results")]
            public List<Result> Results { get; set; } = new List<Result>();
        }

        public class Result
        {
            [JsonPropertyName("regularMarketPrice")]
            public decimal RegularMarketPrice { get; set; }
        }
    }
}
