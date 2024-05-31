using StockQuoteAlert;

class Program
{
    static async Task Main(string[] args)
    {

        if (args.Length != 3)
        {
            Console.WriteLine("Usage: Go to the bin > Debug > net8.0 folder \n and type: dotnet StockQuoteAlert.dll <stock> <selling_price> <buying_price>");
            args = new string[] { "AAPL", "100", "80" };
        }

        string stock_assets = args[0];
        decimal sellingPrice, buyingPrice;

        if (!decimal.TryParse(args[1], out sellingPrice) || !decimal.TryParse(args[2], out buyingPrice))
        {
            Console.WriteLine("Prices must be valid numbers.");
            return;
        }

        string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));

        string configPath = Path.Combine(projectDirectory, "Configs.json");
        if (!File.Exists(configPath))
        {
            Console.WriteLine("Configuration file not found.");
            return;
        }

        EmailConfig config = EmailConfig.Read(configPath);
        QuoteService quoteService = new QuoteService();
        EmailService emailService = new EmailService(config);
        StockMonitor stockMonitor = new StockMonitor(quoteService, emailService, stock_assets, sellingPrice, buyingPrice);
        
        await stockMonitor.Monitor();
    }
}
