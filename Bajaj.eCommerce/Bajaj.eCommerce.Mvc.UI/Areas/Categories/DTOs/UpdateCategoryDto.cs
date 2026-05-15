namespace Bajaj.eCommerce.Mvc.UI.Areas.Categories.DTOs;

public class UpdateCategoryDto
{
    public int CategoryId {  get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
