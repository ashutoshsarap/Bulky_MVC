using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Implement your email sending logic here using your preferred email service provider
            // For example, you can use SMTP, SendGrid, or any other email sending service
            // This is a placeholder implementation. You should replace it with actual email sending code.
            //Console.WriteLine($"Sending email to: {email}");
            //Console.WriteLine($"Subject: {subject}");
            //Console.WriteLine($"Message: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
