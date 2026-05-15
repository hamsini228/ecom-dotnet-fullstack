using AutoMapper;
using eCommerce.Application.DTOs.Invoice;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class InvoiceProfile:Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<CreateInvoiceDto, Invoice>();
        CreateMap<UpdateInvoiceDto, Invoice>();
    }
}
