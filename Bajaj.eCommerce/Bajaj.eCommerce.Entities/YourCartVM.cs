namespace Bajaj.eCommerce.Entities;

public class YourCartVM
{
    public int CartId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int CartItemId { get; set; }  
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Picture { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public int Size { get; set; }
    public int Discount { get; set; }
    public double TotalPrice => (Price - (Price * Discount / 100)) * Quantity;
}
