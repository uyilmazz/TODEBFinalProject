using Business.Abstract;
using Business.Configuration.Filters.Auth;
using Dto.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _messageService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("id")]
        [Permission("Admin,User")]
        public IActionResult Get(int id)
        {
            var result = _messageService.GetDetail(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("new")]
        [Permission("Admin,User")]
        public IActionResult GetNewMessages()
        {
            var result = _messageService.GetNewMessages();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("readed")]
        [Permission("Admin")]
        public IActionResult GetReadedMessages()
        {
            var result = _messageService.ReadedMessages();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("unReaded")]
        [Permission("Admin")]
        public IActionResult GetUnReadedMessages()
        {
            var result = _messageService.UnReadedMessages();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("senderId")]
        [Permission("Admin,User")]
        public IActionResult GetAllBySenderId(int id)
        {
            var result = _messageService.GetAllDetailBySenderId(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost]
        [Permission("Admin,User")]
        public IActionResult Post([FromBody] CreateMessageDto createMessageDto) 
        {
            var result = _messageService.Add(createMessageDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Permission("Admin")]
        public IActionResult Delete(int id)
        {
            var result = _messageService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
