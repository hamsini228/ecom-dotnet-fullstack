using AutoMapper;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Mvc.UI.Areas.Categories.DTOs;
namespace Bajaj.eCommerce.Mvc.UI.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}
