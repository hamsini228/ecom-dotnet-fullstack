using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Carts;
using Bajaj.eCommerce.Entities;

namespace Bajaj.eCommerce.Api.Profiles;

public class CartProfile:Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDto>();
    }
}
