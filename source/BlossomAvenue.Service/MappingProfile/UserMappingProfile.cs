using AutoMapper;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.UsersService.Dtos;

namespace BlossomAvenue.Service.MappingProfile
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.UserRoleName, opt => opt.MapFrom(src => src.UserRole.UserRoleName));

            CreateMap<UserDto, User>();

            CreateMap<User, UserDetailedDto>()
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

            CreateMap<UsersService.Dtos.CreateUpdateUserDto, User>();

            CreateMap<CreateDetailedUserDto, User>()
            .ForMember(dest => dest.UserAddresses, opt => opt.MapFrom(src => new List<UserAddress>
            {
                new UserAddress
                {
                    Address = new AddressDetail
                    {
                        AddressLine1 = src.AddressLine1,
                        AddressLine2 = src.AddressLine2,
                        CityId = src.CityId
                    }
                }
            }))
            .ForMember(dest => dest.UserContactNumbers, opt => opt.MapFrom(src => src.ContactNumbers.Select(cn => new UserContactNumber
            {
                ContactNumber = cn
            }).ToList()));

            CreateMap<CreateDetailedUserDto , CreateDetailedUserResponseDto> ();
        }
    }
}
