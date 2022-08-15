using BackgroundJobs.Abstract;
using Core.Utilities.Results;
using Dto.Concrete;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobs.Concrete
{
    public class SendMailService : ISendMailService
    {
        private readonly IConfiguration _configuration;

        public SendMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IResult SendMail(string toMail, string subject, string content)
        {

            try
            {
                var smtpMail = _configuration.GetSection("MailingService").Get<SmtpMailDto>();
                smtpMail.ToAddress = toMail;

                using (var client = new SmtpClient(smtpMail.SmtpHost, int.Parse(smtpMail.SmtpPort)))
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(smtpMail.FromAddress, smtpMail.Password);

                    MailMessage message = new MailMessage();

                    message.From = new MailAddress(smtpMail.FromAddress);
                    message.To.Add(smtpMail.ToAddress);
                    message.Body = content;
                    message.Subject = subject;
                    message.IsBodyHtml = true;

                    client.Send(message);
                    return new SuccessResult("Email sent successfully.");
                }
            }
            catch (System.Exception ex)
            {
                return new ErrorResult("An error occurred while sending the mail! : " + ex.Message);
            }

        }
    }
}
