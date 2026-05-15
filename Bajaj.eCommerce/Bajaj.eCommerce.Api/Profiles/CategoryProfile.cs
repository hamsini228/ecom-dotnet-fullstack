using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Categories;
using Bajaj.eCommerce.Entities;

namespace Bajaj.eCommerce.Api.Profiles;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>().ReverseMap();
        CreateMap<UpdateCategoryDto, Category>().ReverseMap();
        
    }
}
