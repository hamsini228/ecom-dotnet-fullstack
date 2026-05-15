using AutoMapper;
using eCommerce.Application.DTOs.CartItem;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class CartItemProfile:Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItem,CartItemDto>();
        CreateMap<CreateCartItemDto,CartItem>();
        CreateMap<UpdateCartItemDto,CartItem>();
    }
}
