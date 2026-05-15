using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Invoices;
using Bajaj.eCommerce.Entities;

namespace Bajaj.eCommerce.Api.Profiles;

public class InvoiceProfile:Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceDto>();
    }
}
