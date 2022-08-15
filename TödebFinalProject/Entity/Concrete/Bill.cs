using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Bill : IEntity
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public bool IsPaid { get; set; }
    }
}
