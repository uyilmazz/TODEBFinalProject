using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.FluentValidation;
using Business.Contants.Message;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IMessageDal _messageDal;
        private readonly IMapper _mapper;
        public MessageService(IMessageDal messageDal, IMapper mapper)
        {
            _messageDal = messageDal;
            _mapper = mapper;
        }

        public IResult Add(CreateMessageDto createMessageDto)
        {
            var validator = new CreateMessageValidator();
            var valid = validator.Validate(createMessageDto);

            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(messageText);
            }

            var message = _mapper.Map<Message>(createMessageDto);
            _messageDal.Add(message);
            return new SuccessResult(ResultMessage.MessageCreated);
        }

        public IResult Delete(int id)
        {
            var message = _messageDal.Get(m => m.Id == id);
            if(message is null)
            {
                return new ErrorResult(ResultMessage.MessageNotFound);
            }
            return new SuccessResult(ResultMessage.MessageDeleted);
        }

        public IDataResult<MessageDetailDto> GetDetail(int id)
        {
            var messageDetail = _messageDal.GetMessageDetail(m => m.Id == id);
            if(messageDetail is null)
            {
                return new ErrorDataResult<MessageDetailDto>(ResultMessage.MessageNotFound);
            }

            if (!messageDetail.IsRead)
            {
                messageDetail.IsRead = true;
                var message = _messageDal.Get(m => m.Id == id);
                message.IsRead = true;
                _messageDal.Update(message);
            }

            return new SuccessDataResult<MessageDetailDto>(messageDetail);
        }

        public IDataResult<List<MessageDetailDto>> GetAll()
        {
            return new SuccessDataResult<List<MessageDetailDto>>(_messageDal.GetAllMessageDetailDto());
        }

        public IDataResult<List<MessageDetailDto>> GetNewMessages()
        {
            var date = DateTime.Now.AddDays(-1);
            return new SuccessDataResult<List<MessageDetailDto>>(_messageDal.GetAllMessageDetailDto(m => m.IsRead == false && m.CreatedDate >= date));
        }

        public IDataResult<List<MessageDetailDto>> ReadedMessages()
        {
            return new SuccessDataResult<List<MessageDetailDto>>(_messageDal.GetAllMessageDetailDto(m => m.IsRead == true));
        }

        public IDataResult<List<MessageDetailDto>> UnReadedMessages()
        {
            return new SuccessDataResult<List<MessageDetailDto>>(_messageDal.GetAllMessageDetailDto(m => m.IsRead == false));
        }

        public IResult Update(UpdateMessageDto updateMessageDto)
        {
            var validator = new UpdateMessageValidator();
            var valid = validator.Validate(updateMessageDto);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var dbMessage = _messageDal.Get(m => m.Id == updateMessageDto.Id);
            if(dbMessage is null)
            {
                return new ErrorResult(ResultMessage.MessageNotFound);
            }
            dbMessage = _mapper.Map<Message>(updateMessageDto);
            _messageDal.Update(dbMessage);
            return new SuccessResult(ResultMessage.MessageUpdated);
        }

        public IDataResult<List<MessageDetailDto>> GetAllDetailBySenderId(int id)
        {
            return new SuccessDataResult<List<MessageDetailDto>>(_messageDal.GetAllMessageDetailDto(m => m.SenderId == id));
        }
    }
}
