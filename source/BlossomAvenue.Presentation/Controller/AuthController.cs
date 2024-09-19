using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Service.AuthenticationService;
using BlossomAvenue.Service.CustomExceptions;
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
                else throw new UnAuthorizedException("Invalid refresh or access token!");
            }

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                throw new UnAuthorizedException("Invalid or missing access token!");
            }
            else if (refreshTokenCookie == null)
            {
                throw new UnAuthorizedException("Refresh token is missing");
            }
            throw new UnAuthorizedException("Missing refresh or access token!");
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
                else throw new UnAuthorizedException("Invalid refresh or access token!");
            }
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                throw new UnAuthorizedException("Invalid or missing access token!");
            }
            else if (refreshTokenCookie == null)
            {
                throw new UnAuthorizedException("Refresh token is missing");
            }
            throw new UnAuthorizedException("Missing refresh or access token!");
        }

        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiredAt,
                SameSite = SameSiteMode.None,
                Secure = true,

            };
            Response.Cookies.Append("blossom_avenue_rf_token", refreshToken.Token, cookieOptions);
        }

        private void RemoveRefreshToken()
        {
            Response.Cookies.Delete("blossom_avenue_rf_token");
        }





    }
}
