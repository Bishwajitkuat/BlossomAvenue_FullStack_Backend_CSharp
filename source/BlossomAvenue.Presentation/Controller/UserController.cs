using BlossomAvenue.Service.UsersService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(
            [FromQuery] Guid? userRoleId, 
            [FromQuery] string? search, 
            [FromQuery] int pageNo = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string orderWith="lastName", 
            [FromQuery] string orderBy="ASC")
        {
            if (!(
                orderWith == "firstName" || 
                orderWith == "lastName" || 
                orderWith == "roleName")) 
                throw new ArgumentException("Invalid orderWith parameter");

            if (!(orderBy == "ASC" || orderBy == "DESC")) 
                throw new ArgumentException("Invalid orderBy parameter");

            if (pageNo == 0) throw new ArgumentException("Invalid pageNo parameter");

            if (pageSize == 0) throw new ArgumentException("Invalid pageSize parameter");

            var users = await _userManagement.GetUsers(pageNo, pageSize, userRoleId, orderWith, orderBy, search);

            return Ok(users);
        }

        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetUser(Guid profileId)
        {
            var user = await _userManagement.GetUser(profileId);
            return Ok(user);
        }
        
        [HttpPatch("profileStatus")]
        public async Task<IActionResult> ActiveInactiveUser([FromQuery] Guid userId, [FromQuery] bool status)
        {
            await _userManagement.ActiveInactiveUser(userId, status);
            return NoContent();
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            //Validate user model
            if (!ModelState.IsValid) throw new ArgumentException(String.Join(" | ",ModelState.Values.SelectMany(e => e.Errors)));

            var createdUser = await _userManagement.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { profileId = createdUser.UserId }, createdUser);
        }
        [HttpPost("profile")]
        public async Task<IActionResult> CreateProfile(CreateDetailedUserDto profile)
        {
            var createdProfile = await _userManagement.CreateProfile(profile);
            return CreatedAtAction(nameof(GetUser), new { profileId = createdProfile.UserId }, createdProfile);
        }
    }
}
