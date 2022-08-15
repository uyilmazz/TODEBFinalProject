using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class CreateApartmentDto
    {
        public int BlocId { get; set; }
        public bool IsEmpty { get; set; } = true;
        public int TypeId { get; set; }
        public int Floor { get; set; }
        public int ApartmentNumber { get; set; }
        public int? PersonId { get; set; }

    }
}

