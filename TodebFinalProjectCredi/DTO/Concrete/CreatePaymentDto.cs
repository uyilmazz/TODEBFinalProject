using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete
{
    public class CreatePaymentDto
    {
        public string CardId { get; set; }
        public double Amount { get; set; }
        public int PaidId { get; set; }
    }
}
