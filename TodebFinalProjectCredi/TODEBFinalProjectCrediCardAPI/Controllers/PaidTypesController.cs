using Business.Abstract;
using DTO.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using MongoDB.Bson;

namespace TODEBFinalProjectCrediCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaidTypesController : ControllerBase
    {
        private readonly IPaidTypeService _paidTypeService;

        public PaidTypesController(IPaidTypeService paidTypeService)
        {
            _paidTypeService = paidTypeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _paidTypeService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult Get(string id)
        {
            var result = _paidTypeService.Get(new ObjectId(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreatePaidTypeDto createPaidTypeDto)
        {
            var result = _paidTypeService.Add(createPaidTypeDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(PaidType paidType)
        {
            var result = _paidTypeService.Update(paidType);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var result = _paidTypeService.Delete(new ObjectId(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
