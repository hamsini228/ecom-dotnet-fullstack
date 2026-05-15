using Bajaj.eCommerce.Entities;

namespace Bajaj.eCommerce.Api.DTOs.Invoices;

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public int CartId { get; set; }
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
}
