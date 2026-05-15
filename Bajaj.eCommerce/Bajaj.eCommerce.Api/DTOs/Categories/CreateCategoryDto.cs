using System.ComponentModel.DataAnnotations;

namespace Bajaj.eCommerce.Api.DTOs.Categories;

public class CreateCategoryDto
{
    [Display(Name = "Category Name")]
    [Required(ErrorMessage = "Category Name is required.")]
    [MaxLength(50, ErrorMessage = "Category Name cannot Exceed 50 character")]
    public string CategoryName { get; set; } = string.Empty;
    [Display(Name = "Category Description")]
    [Required(ErrorMessage = "Category Description is required.")]
    [MaxLength(200, ErrorMessage = "Category Description cannot Exceed 200 character")]
    public string Description { get; set; } = string.Empty;
}
