using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public Person Person { get; set; }
    }
}
