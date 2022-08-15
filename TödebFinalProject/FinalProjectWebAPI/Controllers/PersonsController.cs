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
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _personService.GetAllDetail();
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
            var result = _personService.GetDetail(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Permission("Admin")]
        public IActionResult Post([FromBody] CreatePersonDto createPersonDto)
        {
            var result = _personService.Add(createPersonDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Permission("Admin")]
        public IActionResult Update([FromBody] UpdatePersonDto updatePersonDto)
        {
            var result = _personService.Update(updatePersonDto);
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
            var result = _personService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
