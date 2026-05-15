using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Invoices;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Bajaj.eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly ICommonRepository<Invoice> _invoiceRepository;
    private readonly IMapper _mapper;

    public InvoicesController(ICommonRepository<Invoice> invoiceRepository, IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<List<InvoiceDto>>> GetAllInvoices()
    {
        var invoices = await _invoiceRepository.GetAllAsync();
        if (invoices.Count > 0)
        {
            return Ok(_mapper.Map<List<InvoiceDto>>(invoices));
        }
        else
        {
            return NoContent();
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoiceDetails(int id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice != null)
        {
            return Ok(_mapper.Map<InvoiceDto>(invoice));
        }
        else
        {
            return NoContent();
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("cart/{cartId:int}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoiceByCartId(int cartId)
    {
        var invoices = await _invoiceRepository.GetAllAsync();
        var invoice =invoices.FirstOrDefault(i=> i.CartId==cartId);
        if (invoice != null)
            return Ok(_mapper.Map<InvoiceDto>(invoice));
        else
            return NotFound("No invoice found for this cartId");
    }
}
