using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class UpdateFeeDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int ApartmentId { get; set; }
        public bool IsPaid { get; set; }
    }
}
