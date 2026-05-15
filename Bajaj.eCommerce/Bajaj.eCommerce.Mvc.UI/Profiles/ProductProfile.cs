using AutoMapper;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Mvc.UI.Areas.Products.DTOs;
namespace Bajaj.eCommerce.Mvc.UI.Profiles;

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
