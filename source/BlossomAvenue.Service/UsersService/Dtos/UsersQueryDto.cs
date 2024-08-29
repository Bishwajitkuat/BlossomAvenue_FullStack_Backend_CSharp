using BlossomAvenue.Service.Shared_Dtos;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class UsersQueryDto : SharedPagination
    {
        public Guid? UserRoleId { get; set; }

        public new UsersOrderWith OrderWith { get; set; } = UsersOrderWith.LastName;

    }
}
