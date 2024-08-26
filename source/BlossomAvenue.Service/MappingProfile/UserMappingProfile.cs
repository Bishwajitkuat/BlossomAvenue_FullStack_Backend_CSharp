using AutoMapper;

namespace BlossomAvenue.Service.MappingProfile
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Core.Users.User, UsersService.UserDto>();
            CreateMap<UsersService.UserDto, Core.Users.User>();
        }
    }
}
