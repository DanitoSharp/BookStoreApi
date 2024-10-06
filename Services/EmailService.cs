using System;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookStoreApi.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendResetPasswordEmail(string email, string link)
    {
        var fromAddress = "no-reply@BookStoreApi.com";
        var subject = "Password Reset Request";
        var message = $"Click here to reset your password: <a href=\"{link}\">Reset Password</a>";

        // Use SendGrid or SMTP to send the email
        // Example with SendGrid:
        var apiKey = _configuration.GetSection("EmailServiceSendGrid")["ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromAddress, "BookStoreApi");
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
        await client.SendEmailAsync(msg);
    }
}

