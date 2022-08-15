using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class MessageDetailDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string  Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
        public string UserName { get; set; }
    }
}
