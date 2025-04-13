using MailKitSmtpClient = MailKit.Net.Smtp.SmtpClient;
using SystemNetSmtpClient = System.Net.Mail.SmtpClient;
using MimeKit;

namespace MvcHotelReservation.service;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string message, byte[] pdfContent, string pdfFileName)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("FUTURE PROFIT", "futureprofit3@gmail.com"));
        email.To.Add(new MailboxAddress("SALHI ABDELMOUNAIM",to));
        email.Subject = subject;

        var textPart = new TextPart("plain")
        {
            Text = message
        };
        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(new MemoryStream(pdfContent)),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = pdfFileName
        };

        // Combine text and attachment into a multipart message
        var multipart = new Multipart("mixed")
        {
            textPart,
            attachment
        };

        email.Body = multipart;

        using (var client = new MailKitSmtpClient())
        {
            await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), true);
            await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
    }

    public async Task SendEmailAsync(string? to, string subject, string message)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("FUTURE PROFIT", "futureprofit3@gmail.com"));
        email.To.Add(new MailboxAddress("SALHI ABDELMOUNAIM",to));
        email.Subject = subject;

        var body = new TextPart("plain")
        {
            Text = message
        };
        email.Body = body;

        using (var client = new MailKitSmtpClient())
        {
            await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), true);
            await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
        
    }
}
