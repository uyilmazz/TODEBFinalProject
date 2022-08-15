using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class SmtpMailDto
    {
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string FromAddress { get; set; }
        public string Password { get; set; }
        public string ToAddress { get; set; }
    }
}
