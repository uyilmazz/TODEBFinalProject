using Business.Abstract;
using DTO.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using MongoDB.Bson;
using System;
using Newtonsoft.Json;
using System.Net.Http;

namespace TODEBFinalProjectCrediCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _paymentService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult Get(string id)
        {
            var result = _paymentService.Get(new ObjectId(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(PaymentDto PaymentDto)
        {

            var result = _paymentService.Add(PaymentDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPut]
        public IActionResult Update(Payment payment)
        {
            var result = _paymentService.Update(payment);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var result = _paymentService.Delete(new ObjectId(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
