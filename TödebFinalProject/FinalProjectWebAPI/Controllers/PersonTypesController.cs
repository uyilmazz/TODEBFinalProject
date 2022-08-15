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
    public class PersonTypesController : ControllerBase
    {
        private readonly IPersonTypeService _personTypeService;

        public PersonTypesController(IPersonTypeService personTypeService)
        {
            _personTypeService = personTypeService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _personTypeService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("id")]
        [Permission("Admin")]
        public IActionResult Get(int id)
        {
            var result = _personTypeService.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost()]
        [Permission("Admin")]
        public IActionResult Post([FromBody] CreatePersonTypeDto createPersonTypeDto)
        {
            var result = _personTypeService.Add(createPersonTypeDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Permission("Admin")]
        public IActionResult Update([FromBody] UpdatePersonTypeDto updatePersonTypeDto)
        {
            var result = _personTypeService.Update(updatePersonTypeDto);
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
            var result = _personTypeService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
