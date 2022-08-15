using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Dto.Concrete;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : EfEntityRepository<Message, TODEBFinalProjectContext>, IMessageDal
    {
        public List<MessageDetailDto> GetAllMessageDetailDto(Expression<Func<Message, bool>> filter = null)
        {
            using (var context = new TODEBFinalProjectContext())
            {
              
                return (filter != null ? context.Messages.Where(filter) : context.Messages).Include(m => m.Person).Select(message => new MessageDetailDto()
                {
                    Id = message.Id,
                    Content = message.Content,
                    Subject = message.Subject,
                    CreatedDate = message.CreatedDate,
                    IsRead = message.IsRead,
                    UserName = message.Person.FirstName + " " + message.Person.LastName
                }).ToList();
            }
        }

        public MessageDetailDto GetMessageDetail(Expression<Func<Message, bool>> filter)
        {
           using(var context = new TODEBFinalProjectContext())
            {
                return context.Messages.Where(filter).Include(m => m.Person).Select(message => new MessageDetailDto()
                {
                  Id = message.Id,
                  Content = message.Content,
                  Subject=message.Subject,
                  CreatedDate = message.CreatedDate,
                  IsRead = message.IsRead,
                  UserName = message.Person.FirstName + " " + message.Person.LastName
                }).FirstOrDefault();
            }
        }
    }
}
