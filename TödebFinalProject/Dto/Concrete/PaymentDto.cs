using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Concrete
{
    public class PaymentDto
    {
        public int PaidId { get; set; }
        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public int Cvc { get; set; }
        public double Amount { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public string PaidTypeName { get; set; }
    }
}
