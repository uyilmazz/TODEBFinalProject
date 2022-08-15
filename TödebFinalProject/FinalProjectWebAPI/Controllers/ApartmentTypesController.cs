using Business.Abstract;
using Business.Configuration.Filters.Auth;
using Dto.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentTypesController : ControllerBase
    {
        private readonly IApartmentTypeService _apartmentTypeService;

        public ApartmentTypesController(IApartmentTypeService apartmentTypeService)
        {
            _apartmentTypeService = apartmentTypeService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _apartmentTypeService.GetAll();
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
            var result = _apartmentTypeService.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost()]
        [Permission("Admin")]
        public IActionResult Post([FromBody] CreateApartmentTypeDto createApartmentTypeDto)
        {
            var result = _apartmentTypeService.Add(createApartmentTypeDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Permission("Admin")]
        public IActionResult Update([FromBody] ApartmentType updateApartmentType)
        {
            var result = _apartmentTypeService.Update(updateApartmentType);
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
            var result = _apartmentTypeService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
