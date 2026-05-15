namespace Bajaj.eCommerce.Api.DTOs.Products;

public class ProductPagedDto
{
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public List<ProductDto> Products { get; set; }
}
