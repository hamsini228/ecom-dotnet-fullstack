namespace Bajaj.eCommerce.Mvc.UI.Areas.Products.DTOs;

public class UpdateProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Picture { get; set; }
}
