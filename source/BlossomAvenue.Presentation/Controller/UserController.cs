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
        public async Task<IActionResult> GetUsers(Guid? userRoleId, string? search, int pageNo = 1, int pageSize = 10, string orderWith="lastName", string orderBy="ASC")
        {
            var users = await _userManagement.GetUsers(pageNo, pageSize, userRoleId, orderWith, orderBy, search);

            return Ok(users);
        }
    }
}
