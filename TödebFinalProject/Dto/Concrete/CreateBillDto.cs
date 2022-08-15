using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class CreateBillDto
    {
        public double Amount { get; set; }
        public int ApartmentId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
