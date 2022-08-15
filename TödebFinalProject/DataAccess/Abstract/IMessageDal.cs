using Core.DataAccess;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IMessageDal : IEntityRepository<Message>
    {
        MessageDetailDto GetMessageDetail(Expression<Func<Message, bool>> filter);
        List<MessageDetailDto> GetAllMessageDetailDto(Expression<Func<Message, bool>> filter = null);
    }
}
