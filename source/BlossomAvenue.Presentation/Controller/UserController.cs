using BlossomAvenue.Service.SharedDtos;
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
        public async Task<ActionResult<PaginatedResponse<ReadUserDto>>> GetUsers(
            [FromQuery] UsersQueryDto query)
        {
            var paginatedUsers = await _userManagement.GetUsers(query);
            var readUsers = paginatedUsers.Items.Select(u => new ReadUserDto(u)).ToList();
            var readPaginatedUsers = new PaginatedResponse<ReadUserDto>(readUsers, paginatedUsers.ItemPerPage, paginatedUsers.CurrentPage, paginatedUsers.TotalItemCount);
            return Ok(readPaginatedUsers);
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
        public async Task<ActionResult<ReadUserDto>> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _userManagement.UpdateUser(userId, updateUserDto);
            var readUser = new ReadUserDto(user);
            return Ok(readUser);
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
        public async Task<ActionResult<ReadUserProfileDto>> GetUserProfile()
        {
            var userId = GetUserIdFromClaim();
            var user = await _userManagement.GetUser(userId);
            var userReadDto = new ReadUserProfileDto(user);
            return Ok(userReadDto);
        }

        [Authorize]
        [HttpPatch("profile")]
        public async Task<ActionResult<ReadUserProfileDto>> UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            var userId = GetUserIdFromClaim();
            if (userId != updateUserProfileDto.UserId) return Unauthorized();
            var user = await _userManagement.UpdateUserProfile(updateUserProfileDto);
            var readUserProfile = new ReadUserProfileDto(user);
            return Ok(readUserProfile);
        }


        private Guid GetUserIdFromClaim()
        {
            var claims = HttpContext.User;
            var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();
            return new Guid(userId.Value);
        }


    }
}
