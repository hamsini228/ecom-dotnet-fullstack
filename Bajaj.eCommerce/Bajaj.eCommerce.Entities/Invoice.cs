namespace Bajaj.eCommerce.Entities;


public class Invoice
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
}

