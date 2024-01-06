using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using Twitter.Business.ExternalServices.Interfaces;

namespace Twitter.Business.ExternalServices.Implements
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public EmailService(IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }

        public void Send(string toMail, string header, string body, bool isHtml = true)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);
            MailAddress from = new MailAddress(_configuration["Email:Username"], "Developer Team");
            MailAddress to = new MailAddress(toMail);

            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.Subject = header;
            message.IsBodyHtml = isHtml;

            smtp.Send(message);
        }

        public void SendMail()
        {
            _emailService.Send("adilibadov456@gmail.com", "Welcome", "This is test mail");

        }
    }
}


