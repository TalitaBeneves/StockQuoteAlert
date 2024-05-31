using static StockQuoteAlert.QuoteResult;

namespace StockQuoteAlert
{
    public class StockMonitor
    {
        private readonly QuoteService _quoteService;
        private readonly EmailService _emailService;
        private readonly string _stock_assets;
        private readonly decimal _sellingPrice;
        private readonly decimal _buyingPrice;

        public StockMonitor(QuoteService quoteService, EmailService emailService, string stock_assets, decimal sellingPrice, decimal buyingPrice)
        {
            _quoteService = quoteService;
            _emailService = emailService;
            _stock_assets = stock_assets;
            _sellingPrice = sellingPrice;
            _buyingPrice = buyingPrice;
        }

        public async Task Monitor()
        {
            while (true)
            {
                try
                {
                    Quote quote = await _quoteService.GetQuote(_stock_assets);

                    if (quote == null || quote.Results == null || quote.Results.Count == 0)
                    {
                        Console.WriteLine($"[{DateTime.Now}] Error getting quote for {_stock_assets}");
                        await Task.Delay(60000);
                        continue;
                    }

                    var regularMarketPrice = quote.Results[0].RegularMarketPrice;

                    Console.WriteLine($"[{DateTime.Now}] {_stock_assets} Current price: {regularMarketPrice}");

                    if (quote == null)
                    {
                        Console.WriteLine($"Error getting quote for {_stock_assets}");
                        return;
                    }

                    if (regularMarketPrice > _sellingPrice)
                        _emailService.SendEmail(_stock_assets, "VENDA da ação: ", _sellingPrice, $"Stock {_stock_assets} reached selling price of {regularMarketPrice} ");
                    else if (regularMarketPrice < _buyingPrice)
                        _emailService.SendEmail(_stock_assets, "COMPRA da ação: ", _sellingPrice, $"Stock {_stock_assets} reached buying price of {regularMarketPrice}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now}] Erro: {ex.Message}");
                }

                await Task.Delay(60000);
            }
        }
    }
}
