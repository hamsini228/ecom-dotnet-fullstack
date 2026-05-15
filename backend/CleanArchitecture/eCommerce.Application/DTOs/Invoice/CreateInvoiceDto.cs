namespace eCommerce.Application.DTOs.Invoice;

public class CreateInvoiceDto
{
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public int CartId { get; set; }
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
}
