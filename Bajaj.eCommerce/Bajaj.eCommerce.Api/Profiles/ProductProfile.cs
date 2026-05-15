using AutoMapper;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Api.DTOs.Products;
namespace Bajaj.eCommerce.Api.Profiles;

public class ProductProfile:Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product,ProductDetailsDTO>();

    }
}
