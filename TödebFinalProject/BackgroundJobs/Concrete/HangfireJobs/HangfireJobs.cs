using BackgroundJobs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobs.Concrete.HangfireJobs
{
    public class HangfireJobs : IJobs
    {
        private ISendMailService _sendMailService;

        public HangfireJobs(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
        }


        public void FireAndForget(string toMail, string subject, string content)
        {
            Hangfire.BackgroundJob.Enqueue(() => _sendMailService.SendMail( toMail,  subject,  content));
        }

    }
}
