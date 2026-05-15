using AutoMapper;
using eCommerce.Application.DTOs.User;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
