namespace Bajaj.eCommerce.Api.DTOs.Products;

public class ProductDto
{
    public  int ProductId { get; set; }
    public string ProductName { get; set; }= string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public double UnitPrice {  get; set; } 
    public string Picture { get; set; }
    public int Discount { get; set; }
    public string MadeIn { get; set; }
    public string ShoeType { get; set; }
    public string WarrantyPeriod { get; set; }
    public string ReturnPolicy { get; set; }
}
