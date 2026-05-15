namespace Bajaj.eCommerce.Entities;

public class InvoiceItem
{
    public int InvoiceItemId { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }

    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public double TotalPrice { get; set; }
}