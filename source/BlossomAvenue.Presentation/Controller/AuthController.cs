using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Service.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            var result = await _authService.Login(loginRequest.Username, loginRequest.Password);

            if (!result.LoginResponseDto.IsAuthenticated)
            {
                return Unauthorized();
            }
            SetRefreshToken(result.RefreshToken);
            return Ok(result.LoginResponseDto);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _authService.Logout(token);
        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiredAt
            };
            Response.Cookies.Append("blossom_avenue_rf_token", refreshToken.Token, cookieOptions);
        }

        private void RemoveRefreshToken()
        {
            Response.Cookies.Delete("blossom_avenue_rf_token");
        }

    }
}
