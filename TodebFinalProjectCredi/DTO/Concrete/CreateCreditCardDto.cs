using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete
{
    public class CreateCreditCardDto
    {
        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public int Cvc { get; set; }
        public double Balance { get; set; } = 0;
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
    }
}
