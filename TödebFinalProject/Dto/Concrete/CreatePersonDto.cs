using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class CreatePersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TCNo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PlakaNo { get; set; }
        public int TypeId { get; set; } = 2;

    }
}
