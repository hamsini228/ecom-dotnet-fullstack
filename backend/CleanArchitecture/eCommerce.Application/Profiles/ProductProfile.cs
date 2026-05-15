using AutoMapper;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class ProductProfile :Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
