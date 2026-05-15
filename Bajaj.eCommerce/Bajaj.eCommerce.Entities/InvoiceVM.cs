namespace Bajaj.eCommerce.Entities;

public class InvoiceVM
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
    public List<InvoiceItem> Items { get; set; }

    public double OriginalTotal => Items.Sum(x => x.Price * x.Quantity);
    public double DiscountedTotal => Items.Sum(x => x.TotalPrice);
    public double Savings => OriginalTotal - DiscountedTotal;
}