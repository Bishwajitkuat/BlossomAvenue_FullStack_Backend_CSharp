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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers(
            [FromQuery] UsersQueryDto query)
        {

            if (query.PageNo == 0) throw new ArgumentException("Invalid pageNo parameter");

            if (query.PageSize == 0) throw new ArgumentException("Invalid pageSize parameter");

            var users = await _userManagement.GetUsers(query);

            return Ok(users);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid userId)
        {
            var user = await _userManagement.GetUser(userId);
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("profileStatus")]
        public async Task<IActionResult> ActiveInactiveUser([FromQuery] Guid userId, [FromQuery] bool status)
        {
            await _userManagement.ActiveInactiveUser(userId, status);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUpdateUserDto user)
        {
            //Validate user model
            if (!ModelState.IsValid) throw new ArgumentException(String.Join(" | ", ModelState.Values.SelectMany(e => e.Errors)));

            var createdUser = await _userManagement.CreateUser(user);
            return Created(nameof(GetUser), createdUser);
        }
        [Authorize(Policy = "UserIdPolicy")]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] CreateUpdateUserDto user)
        {
            await _userManagement.UpdateUser(userId, user);
            return NoContent();
        }

        // PROFILE

        [HttpPost("profile")]
        public async Task<ActionResult<ReadDetailedUserDto>> CreateProfile(CreateDetailedUserDto profile)
        {
            var newUser = profile.ConvertToUser();
            var createdProfile = await _userManagement.CreateProfile(newUser);
            var readProfile = new ReadDetailedUserDto(createdProfile);
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
            var userReadDto = new ReadDetailedUserDto(user);
            return Ok(userReadDto);
        }

        [Authorize]
        [HttpPatch("profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateDetailedUserDto updateDetailedUserDto)
        {
            var claims = HttpContext.User;
            var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (userId is null || new Guid(userId.Value) != updateDetailedUserDto.UserId) return Unauthorized();
            await _userManagement.UpdateUserProfile(updateDetailedUserDto);
            return NoContent();

        }


    }
}
