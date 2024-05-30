using System;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: Go to the bin > Debug > net8 folder \n and type: dotnet StockQuoteAlert.dll <stock> <selling_price> <buying_price>");
            return;
        }

        string stock_assets = args[0];
        double sellingPrice, buyingPrice;

        if (!double.TryParse(args[1], out sellingPrice) || !double.TryParse(args[2], out buyingPrice))
        {
            Console.WriteLine("Prices must be valid numbers.");
            return;
        }

      
        Console.WriteLine($"Stock: {stock_assets}");
        Console.WriteLine($"Selling price: {sellingPrice}");
        Console.WriteLine($"Buying price: {buyingPrice}");
    }
}
