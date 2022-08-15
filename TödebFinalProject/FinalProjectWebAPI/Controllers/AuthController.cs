using Business.Abstract;
using Dto.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(PersonForLoginDto personForLoginDto)
        {
            var loginResult = _authService.Login(personForLoginDto);
            if (!loginResult.Success)
            {
                return BadRequest(loginResult);
            }

            var tokenResult = _authService.CreateAccessToken(loginResult.Data);
            if (!tokenResult.Success)
            {
                return BadRequest(tokenResult);
            }
            return Ok(tokenResult);
        }
    }
}
