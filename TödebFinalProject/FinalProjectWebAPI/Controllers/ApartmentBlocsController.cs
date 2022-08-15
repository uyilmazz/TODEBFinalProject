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
    public class ApartmentBlocsController : ControllerBase
    {
        private readonly IApartmentBlocService _apartmentBlocService;

        public ApartmentBlocsController(IApartmentBlocService apartmentBlocService)
        {
            _apartmentBlocService = apartmentBlocService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _apartmentBlocService.GetAll();
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
            var result = _apartmentBlocService.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost()]
        [Permission("Admin")]
        public IActionResult Post([FromBody] CreateApartmentBlocDto createApartmentBlocDto)
        {
            var result = _apartmentBlocService.Add(createApartmentBlocDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Permission("Admin")]
        public IActionResult Update([FromBody] ApartmentBloc updateApartmentBloc)
        {
            var result = _apartmentBlocService.Update(updateApartmentBloc);
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
            var result = _apartmentBlocService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
