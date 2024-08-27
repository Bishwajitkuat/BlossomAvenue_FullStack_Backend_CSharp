using BlossomAvenue.Service.UsersService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
