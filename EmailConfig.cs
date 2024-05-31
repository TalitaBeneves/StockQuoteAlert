using System.Text.Json;

namespace StockQuoteAlert
{
    public class EmailConfig
    {
        public string EmailRecipient { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;

        public static EmailConfig Read(string path)
        {
            string configJson = File.ReadAllText(path);
            try
            {
                return JsonSerializer.Deserialize<EmailConfig>(configJson)!;
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing Json: " + ex.Message);
            }
           
        }
    }
}
