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
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _apartmentService.GetAllDetail();
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
            var result = _apartmentService.GetDetail(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Permission("Admin")]
        public IActionResult Post([FromBody] CreateApartmentDto createApartmentDto)
        {
            var result = _apartmentService.Add(createApartmentDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Permission("Admin")]
        public IActionResult Update([FromBody] UpdateApartmentDto updateApartmentDto)
        {
            var result = _apartmentService.Update(updateApartmentDto);
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
            var result = _apartmentService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
