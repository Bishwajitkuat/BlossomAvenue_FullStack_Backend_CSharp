using BlossomAvenue.Service.SharedDtos;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class UsersQueryDto : SharedPagination
    {
        public Guid? UserRoleId { get; set; }

        public new UsersOrderWith OrderUserWith { get; set; } = UsersOrderWith.LastName;

    }
}
