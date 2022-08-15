using Business.Abstract;
using Business.Configuration.Filters.Auth;
using Dto.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinalProjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly HttpClient _httpClient = new HttpClient();

        public BillsController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        [Permission("Admin")]
        public IActionResult GetAll()
        {
            var result = _billService.GetAllDetail();
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
            var result = _billService.GetBillDetailDtoById(id);
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
            var result = _billService.GetAllBillDetailDtoByUserId(userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("unPaid")]
        [Permission("Admin,User")]
        public IActionResult GetAllUnPaidBillDetail()
        {
            var result = _billService.GetAllUnPaidBillDetailDto();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("single")]
        [Permission("Admin")]
        public IActionResult Post([FromBody] CreateBillDto createBillDto)
        {
            var result = _billService.Add(createBillDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("bulkAdd")]
        [Permission("Admin")]
        public IActionResult PostBulk([FromBody] CreateBillDto createBillDto)
        {
            var result = _billService.BulkdAdd(createBillDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("paymentBill")]
        [Permission("Admin,User")]
        public async Task<IActionResult> PaymentBillAsync(PaymentDto paymentDto)
        {
            var unPaidBillResult = _billService.IsUnPaidBill(paymentDto.PaidId);
            if (!unPaidBillResult.Success)
            {
                return BadRequest(unPaidBillResult);
            }
            paymentDto.Amount = unPaidBillResult.Data.Amount;
            paymentDto.PaidTypeName = "Bill";
            var paymentBillResult = await _httpClient.PostAsJsonAsync("https://localhost:44378/api/Payments", paymentDto);
            if (paymentBillResult.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest(paymentBillResult);
            }
            unPaidBillResult.Data.IsPaid = true;
            var updateBillResult = _billService.Update(unPaidBillResult.Data);

            if (!updateBillResult.Success)
            {
                return BadRequest(updateBillResult);
            }
            return Ok(updateBillResult);
        }

        [HttpPatch]
        [Permission("Admin")]
        public IActionResult Update([FromBody] UpdateBillDto updateBillDto)
        {
            var result = _billService.Update(updateBillDto);
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
            var result = _billService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
       
    }
}
