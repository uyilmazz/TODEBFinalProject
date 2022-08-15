using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobs.Abstract
{
    public interface IJobs
    {
        void FireAndForget(string toMail, string subject, string content);
    }
}
