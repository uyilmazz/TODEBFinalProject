using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Apartment : IEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsEmpty { get; set; }
        public int BlocId { get; set; }
        public int TypeId { get; set; }
        public int Floor { get; set; }
        public int ApartmentNumber { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        [ForeignKey("BlocId")]
        public ApartmentBloc ApartmentBloc { get; set; }

        [ForeignKey("TypeId")]
        public ApartmentType ApartmentType { get; set; }
    }
}
