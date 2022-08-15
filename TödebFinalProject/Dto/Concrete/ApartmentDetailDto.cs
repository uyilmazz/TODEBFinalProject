using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class ApartmentDetailDto
    {
        public int Id { get; set; }
        public string BlocName { get; set; }
        public string TypeName { get; set; }
        public string UserName { get; set; }
        public int Floor { get; set; }
        public int ApartmentNumber { get; set; }
        public bool IsEmpty { get; set; }

    }
}


