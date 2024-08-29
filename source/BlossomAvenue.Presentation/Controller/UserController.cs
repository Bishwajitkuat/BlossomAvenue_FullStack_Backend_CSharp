using BlossomAvenue.Service.UsersService;
using Microsoft.AspNetCore.Authorization;
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

        
        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetUser(Guid profileId)
        {
            var user = await _userManagement.GetUser(profileId);
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
            createdProfile.Password = String.Empty;
            return CreatedAtAction(nameof(GetUser), new { profileId = createdProfile.UserId }, createdProfile);
        }
    }
}
