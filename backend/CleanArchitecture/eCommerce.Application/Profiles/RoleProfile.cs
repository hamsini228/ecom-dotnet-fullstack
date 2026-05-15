using AutoMapper;
using eCommerce.Application.DTOs.Role;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<Role,RoleDto>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();
    }
}
