using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Service.SharedDtos;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class UsersQueryDto : SharedPagination
    {
        public UserRole? UserRole { get; set; }

        public UsersOrderWith? OrderUserWith { get; set; } = UsersOrderWith.CreatedAt;

        public bool? IsActive { get; set; }

    }
}
