using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class CreateMessageDto
    {
        public string Content { get; set; }
        public string Subject { get; set; }
        public int SenderId { get; set; }
    }
}
