using Model.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Payment : DocumentBaseEntity
    {
        public ObjectId CreditCardId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public double Amount { get; set; }
        public int PaidId { get; set; }
        public ObjectId? PaidTypeId { get; set; }
    }
}
