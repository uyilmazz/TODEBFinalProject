using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete
{
    public class PaymentDetailDto
    {
        public ObjectId Id { get; set; }
        public string ObjectId => Id.ToString();
        public string CustomerName { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public int PaidId { get; set; }
        public string PaidType { get; set; }

    }
}
