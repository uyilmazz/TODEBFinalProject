using Core.Utilities.Results;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMessageService
    {
        IDataResult<List<MessageDetailDto>> GetAll();
        IDataResult<MessageDetailDto> GetDetail(int id);
        IDataResult<List<MessageDetailDto>> GetAllDetailBySenderId(int id);
        IDataResult<List<MessageDetailDto>> GetNewMessages();
        IDataResult<List<MessageDetailDto>> UnReadedMessages();
        IDataResult<List<MessageDetailDto>> ReadedMessages();
        IResult Add(CreateMessageDto createMessageDto);
        IResult Update(UpdateMessageDto updateMessageDto);
        IResult Delete(int id);
    }
}
