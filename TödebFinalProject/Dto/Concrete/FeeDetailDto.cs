using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class FeeDetailDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPaid { get; set; }
        public int ApartmentId { get; set; }
        public string PersonName { get; set; }
    }
}
