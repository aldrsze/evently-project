using System;
using System.IO;
using Evently.Core.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Evently.Core.Services
{
    public static class EmailService
    {
        // Sends ticket email with attached QR code via Gmail SMTP
        public static bool SendTicketEmail(string recipientEmail, string attendeeName, string eventName, string venue, DateTime eventDate, string ticketId)
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, "db.config");
            if (!File.Exists(configPath))
            {
                return false;
            }

            string[] lines = File.ReadAllLines(configPath);
            string smtpEmail = DatabaseConnection.ReadValue(lines, "SmtpEmail");
            string smtpPassword = DatabaseConnection.ReadValue(lines, "SmtpPassword");

            if (smtpEmail.Length == 0 || smtpPassword.Length == 0)
            {
                return false;
            }

            try
            {
                byte[] qrBytes = QrCodeService.GenerateTicketQrPng(ticketId);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Evently", smtpEmail));
                message.To.Add(new MailboxAddress(attendeeName, recipientEmail));
                message.Subject = $"Your Ticket for {eventName}";

                var builder = new BodyBuilder
                {
                    TextBody = $"Hi {attendeeName},\n\nHere is your ticket for {eventName}.\nVenue: {venue}\nDate: {eventDate:MMMM d, yyyy h:mm tt}\nTicket ID: {ticketId}\n\nPlease present the attached QR code at the door."
                };
                builder.Attachments.Add("ticket_qr.png", qrBytes, new ContentType("image", "png"));
                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(smtpEmail, smtpPassword);
                client.Send(message);
                client.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SendReviewRequestEmail(string recipientEmail, string attendeeName, string eventName, string reviewUrl)
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, "db.config");
            if (!File.Exists(configPath))
            {
                return false;
            }

            string[] lines = File.ReadAllLines(configPath);
            string smtpEmail = DatabaseConnection.ReadValue(lines, "SmtpEmail");
            string smtpPassword = DatabaseConnection.ReadValue(lines, "SmtpPassword");

            if (smtpEmail.Length == 0 || smtpPassword.Length == 0)
            {
                return false;
            }

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Evently", smtpEmail));
                message.To.Add(new MailboxAddress(attendeeName, recipientEmail));
                message.Subject = $"How was {eventName}? Leave your feedback";

                var builder = new BodyBuilder
                {
                    TextBody = $"Hi {attendeeName},\n\nThank you for attending {eventName}!\n\nWe would love to hear your feedback. Please take a moment to rate the event here:\n{reviewUrl}\n\nYour review helps us improve future events."
                };
                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(smtpEmail, smtpPassword);
                client.Send(message);
                client.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
