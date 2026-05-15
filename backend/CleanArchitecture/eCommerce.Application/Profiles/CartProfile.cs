using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class CartProfile:Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDto>();
        CreateMap<CreateCartDto, Cart>();
        CreateMap<UpdateCartDto, Cart>();
    }
}
