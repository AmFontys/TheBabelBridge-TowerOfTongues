using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace BBTT.Email;

public class EmailAccesor : IEmail
{
    private readonly EmailSettings _settings;
    private EmailClient _client;

    public EmailAccesor (IOptions<EmailSettings> options)
    {
        _settings = options.Value;
        _client = new EmailClient(new Uri(_settings.Endpoint), new AzureKeyCredential(_settings.Key));
    }

    public Task CreateConnection ()
    {
        _client = new EmailClient(new Uri(_settings.Endpoint), new AzureKeyCredential(_settings.Key));
        return Task.CompletedTask;
    }

    public async Task SendEmail (string email, string code)
    {
        var subject = "Your code to Babel Bridge - Tower of Tongues.";
        var htmlContent = $@"
        <html>
          <body style='font-family: Arial, sans-serif; color: #333;'>
            <div style='max-width: 480px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; padding: 32px 24px; background: #fafbfc;'>
              <h2 style='color: #2d6cdf; margin-bottom: 16px;'>Your Verification Code</h2>
              <p>Dear user,</p>
              <p>To complete your sign-in, please use the following verification code:</p>
              <div style='font-size: 2em; font-weight: bold; letter-spacing: 4px; margin: 24px 0; color: #2d6cdf;'>
                {System.Net.WebUtility.HtmlEncode(code)}
              </div>
              <p>This code will expire in 10 minutes. If you did not request this code, please ignore this email.</p>
              <p style='margin-top: 32px;'>Thank you,<br/>The BBTT Team</p>
            </div>
          </body>
        </html>";
        var sender = "DoNotReply@eb348224-6e35-49fe-8965-b2ba4e7817d3.azurecomm.net";        

        /// Send the email message with WaitUntil.Started
        EmailSendOperation emailSendOperation = await _client.SendAsync(
            Azure.WaitUntil.Started,
            sender,
            email,
            subject,
            htmlContent);
        await TrackEmailDelivery(emailSendOperation);
    }

    private static async Task TrackEmailDelivery (EmailSendOperation emailSendOperation)
    {
        /// Call UpdateStatus on the email send operation to poll for the status
        /// manually.
        try
        {
            while (true)
            {
                await emailSendOperation.UpdateStatusAsync();
                if (emailSendOperation.HasCompleted)
                {
                    break;
                }
                await Task.Delay(100);
            }

            if (emailSendOperation.HasValue)
            {
                Console.WriteLine($"Email queued for delivery. Status = {emailSendOperation.Value.Status}");
            }
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Email send failed with Code = {ex.ErrorCode} and Message = {ex.Message}");
        }

        /// Get the OperationId so that it can be used for tracking the message for troubleshooting
        string operationId = emailSendOperation.Id;
        Console.WriteLine($"Email operation id = {operationId}");
    }
}
