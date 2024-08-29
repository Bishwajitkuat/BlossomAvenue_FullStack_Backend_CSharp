using BlossomAvenue.Service.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManagement _authService;

        public AuthController(IAuthManagement authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var result = await _authService.Authenticate(loginRequest.Username, loginRequest.Password);

            if (!result.IsAuthenticated)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {   
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _authService.Logout(token);

            return NoContent();
        }
    }
}
