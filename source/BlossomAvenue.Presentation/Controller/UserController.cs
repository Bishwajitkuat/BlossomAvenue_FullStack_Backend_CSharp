using BlossomAvenue.Service.UsersService;
using BlossomAvenue.Service.UsersService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagement _userManagement;

        public UserController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        // ADMIN
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<ReadUserDto>>> GetUsers(
            [FromQuery] UsersQueryDto query)
        {
            var users = await _userManagement.GetUsers(query);
            var readUsers = users.Select(u => new ReadUserDto(u));
            return Ok(readUsers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}")]
        public async Task<ActionResult<ReadUserDto>> GetUser([FromRoute] Guid userId)
        {
            var user = await _userManagement.GetUser(userId);
            var readUser = new ReadUserDto(user);
            return Ok(readUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            await _userManagement.UpdateUser(userId, updateUserDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            await _userManagement.DeleteUser(userId);
            return NoContent();
        }


        // PROFILE

        [HttpPost("profile")]
        public async Task<ActionResult<ReadUserProfileDto>> CreateProfile(CreateUserProfileDto profile)
        {
            var newUser = profile.ConvertToUser();
            var createdProfile = await _userManagement.CreateProfile(newUser);
            var readProfile = new ReadUserProfileDto(createdProfile);
            return Created(nameof(GetUser), readProfile);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var claims = HttpContext.User;
            var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();
            var user = await _userManagement.GetUser(new Guid(userId.Value));
            var userReadDto = new ReadUserProfileDto(user);
            return Ok(userReadDto);
        }

        [Authorize]
        [HttpPatch("profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            var claims = HttpContext.User;
            var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (userId is null || new Guid(userId.Value) != updateUserProfileDto.UserId) return Unauthorized();
            await _userManagement.UpdateUserProfile(updateUserProfileDto);
            return NoContent();

        }


    }
}
