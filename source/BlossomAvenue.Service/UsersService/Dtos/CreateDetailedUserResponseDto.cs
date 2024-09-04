using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class CreateDetailedUserResponseDto : CreateDetailedUserDto
    {
        public Guid UserId { get; set; }
    }
}
