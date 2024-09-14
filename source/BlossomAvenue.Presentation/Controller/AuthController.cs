using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Service.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
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
        public async Task<IActionResult> Logout()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var refreshTokenCookie = Request.Cookies["blossom_avenue_rf_token"];

            if (authHeader != null && refreshTokenCookie != null && authHeader.StartsWith("Bearer "))
            {
                var jwtToken = authHeader.Substring("Bearer ".Length).Trim();
                var isLoggedOut = await _authService.Logout(jwtToken, refreshTokenCookie);
                if (isLoggedOut)
                {
                    RemoveRefreshToken();
                    return Ok("Thank you! See you soon. You have been logged out from all the sessions.");
                }
            }

            return Unauthorized();

        }


        [HttpGet("refreshToken")]
        public async Task<ActionResult<LoginRequestDto>> GetRefreshToken()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var refreshTokenCookie = Request.Cookies["blossom_avenue_rf_token"];

            if (authHeader != null && refreshTokenCookie != null && authHeader.StartsWith("Bearer "))
            {
                var jwtToken = authHeader.Substring("Bearer ".Length).Trim();
                var result = await _authService.GetRefreshToken(jwtToken, refreshTokenCookie);
                if (result.LoginResponseDto.IsAuthenticated)
                {
                    SetRefreshToken(result.RefreshToken);
                    return Ok(result.LoginResponseDto);
                }
            }
            return Unauthorized();
        }

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
