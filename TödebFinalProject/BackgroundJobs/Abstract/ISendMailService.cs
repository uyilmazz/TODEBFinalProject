using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobs.Abstract
{
    public interface ISendMailService
    {
        IResult SendMail(string toMail, string subject, string content);
    }
}
