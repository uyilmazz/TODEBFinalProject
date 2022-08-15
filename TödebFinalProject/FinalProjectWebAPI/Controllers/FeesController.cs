using Business.Abstract;
using Dto.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Business.Configuration.Filters.Auth;

namespace FinalProjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly HttpClient _httpClient = new HttpClient();
        public FeesController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _feeService.GetAllDetail();
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
            var result = _feeService.GetFeeDetailDtoById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        
        [HttpGet("userId")]
        [Permission("Admin,User")]
        public IActionResult GetFeeDetailByUserId(int userId)
        {
            var result = _feeService.GetAllFeeDetailDtoByUserId(userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("unPaid")]
        [Permission("Admin")]
        public IActionResult GetAllUnPaidFeeDetail()
        {
            var result = _feeService.GetAllUnPaidFeeDetail();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("single")]
        [Permission("Admin")]
        public IActionResult Post([FromBody] CreateFeeDto createFeeDto)
        {
            var result = _feeService.Add(createFeeDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("bulkAdd")]
        [Permission("Admin")]
        public IActionResult PostBulk([FromBody] CreateFeeDto createFeeDto)
        {
            var result = _feeService.BulkdAdd(createFeeDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("paymentFee")]
        [Permission("Admin,User")]
        public async Task<IActionResult> PaymentFeeAsync(PaymentDto paymentDto)
        {
            var unPaidFeeResult = _feeService.IsUnPaidFee(paymentDto.PaidId);
            if (!unPaidFeeResult.Success)
            {
                return BadRequest(unPaidFeeResult);
            }
            paymentDto.Amount = unPaidFeeResult.Data.Amount;
            paymentDto.PaidTypeName = "Fee";
            var paymentFeeResult = await _httpClient.PostAsJsonAsync("https://localhost:44378/api/Payments", paymentDto);
            if (paymentFeeResult.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest(paymentFeeResult);
            }
            unPaidFeeResult.Data.IsPaid = true;
            var updateFeeResult = _feeService.Update(unPaidFeeResult.Data);

            if (!updateFeeResult.Success)
            {
                return BadRequest(updateFeeResult);
            }
            return Ok(updateFeeResult);
        }

        [HttpPatch]
        [Permission("Admin")]
        public IActionResult Update([FromBody] UpdateFeeDto updateFeeDto)
        {
            var result = _feeService.Update(updateFeeDto);
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
            var result = _feeService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
