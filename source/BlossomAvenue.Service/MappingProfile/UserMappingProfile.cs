using AutoMapper;
using BlossomAvenue.Service.UsersService;

namespace BlossomAvenue.Service.MappingProfile
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Core.Users.User, UserDto>().ForMember(dest => dest.UserRoleName, opt => opt.MapFrom(src => src.UserRole.UserRoleName));

            CreateMap<UserDto, Core.Users.User>();

            CreateMap<Core.Users.User, UserDetailedDto>()
                .ForMember(dest => dest.UserRoleName, opt => opt.MapFrom(src => src.UserRole.UserRoleName))
                .ForMember(dest => dest.ContactNumbers, opt => opt.MapFrom(src => src.UserContactNumbers.Select(e => e.ContactNumber).ToArray()))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.UserAddresses.Select(e => new AddressDto
                {
                    AddressId = e.Address != null ? e.Address.AddressId : Guid.Empty,
                    AddressLine1 = e.Address != null ? e.Address.AddressLine1 : String.Empty,
                    AddressLine2 = e.Address != null ? e.Address.AddressLine2 : String.Empty,
                    CityId = e.Address != null ? e.Address.CityId : null,
                    CityName = e.Address != null && e.Address.City != null ? e.Address.City.CityName : String.Empty,
                    IsDefaultAddress = e.DefaultAddress ?? false
                })));

            CreateMap<CreateUserDto, Core.Users.User>();
        }
    }
}
