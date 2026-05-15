using eCommerce.Application.Contracts;
using eCommerce.Domain;

namespace eCommerce.Application.Services;

public class InvoiceService
{
    private readonly ICommonRepository<Invoice> _invoiceRepository;

    public InvoiceService(ICommonRepository<Invoice> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
    {
        return await _invoiceRepository.GetAllAsync();
    }
    public async Task<Invoice> GetInvoiceDetailsAsync(int id)
    {
        return await _invoiceRepository.GetByIdAsync(id);
    }
    public async Task<int> CreateInvoice(Invoice invoice)
    {
        return await _invoiceRepository.AddAsync(invoice);
    }
    public async Task<int> UpdateInvoice(Invoice invoice)
    {
        return await _invoiceRepository.UpdateAsync(invoice);
    }
    public async Task<int> DeleteInvoice(int id)
    {
        return await _invoiceRepository.DeleteAsync(id);
    }
}
