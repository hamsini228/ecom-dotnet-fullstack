namespace Bajaj.eCommerce.Api.DTOs.Products;
public class ProductDetailsDTO
{
    
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public double UnitPrice { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime ManufacturingDate { get; set; }
    public string MadeIn { get; set; } = string.Empty;
    public string ShoeType { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string WarrantyPeriod { get; set; } = string.Empty;
    public string ReturnPolicy { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public string? Picture { get; set; }
}
