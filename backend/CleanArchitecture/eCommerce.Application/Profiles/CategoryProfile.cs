using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}
