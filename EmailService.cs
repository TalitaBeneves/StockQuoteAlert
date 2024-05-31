using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace StockQuoteAlert
{
    public class EmailService
    {
        private readonly EmailConfig _config;
        public EmailService(EmailConfig config)
        {
            _config = config;
        }

        public void SendEmail(string stock_asset, string action, string message)
        {
            var msg = PrepareteMessage(stock_asset, action, message);
            EnviarEmailPorSMTP(msg);
        }

        private MailMessage PrepareteMessage(string stock_asset, string action, string message)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_config.SmtpUser);

            if (validarEmail(_config.EmailRecipient))
                mail.To.Add(_config.EmailRecipient);

            mail.Subject = $"Alert {action} {stock_asset}";
            mail.Body = message;

            mail.IsBodyHtml = true;

            return mail;
        }

        private bool validarEmail(string email)
        {
            Regex reg = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (reg.IsMatch(email))
                return true;

            return false;
        }

        private void EnviarEmailPorSMTP(MailMessage message)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(_config.SmtpServer);
                smtp.Port = _config.SmtpPort;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_config.SmtpUser, _config.SmtpPassword);
                smtp.EnableSsl = true;
                smtp.Send(message);
                smtp.Dispose();

                Console.WriteLine("Email sent successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending email {ex.Message}");
            }

        }
    }
}
