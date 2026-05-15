using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.Invoice;
using eCommerce.Application.Services;
using eCommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[EnableCors("BajajPolicy")]
[Route("api/v{version:apiVersion}/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceService _invoiceService;
    private readonly IMapper _mapper;

    public InvoicesController(InvoiceService invoiceService, IMapper mapper)
    {
        _invoiceService = invoiceService;
        _mapper = mapper;
    }
    //[Authorize(Roles = "Customer,Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoices()
    {
        var invoices = await _invoiceService.GetInvoicesAsync();
        if (invoices.Count() > 0)
        {
            return Ok(_mapper.Map<IEnumerable<InvoiceDto>>(invoices));
        }
        else
        {
            return NoContent();
        }
    }

    //[Authorize(Roles = "Customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoiceDetails(int id)
    {
        var invoice = await _invoiceService.GetInvoiceDetailsAsync(id);
        if (invoice != null)
        {
            return Ok(_mapper.Map<InvoiceDto>(invoice));
        }
        else
        {
            return NoContent();
        }
    }
    //[Authorize(Roles = "Customer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CreateInvoiceDto>> CreateInvoice(CreateInvoiceDto createInvoiceDto)
    {
        if (ModelState.IsValid)
        {
            var invoice = _mapper.Map<Invoice>(createInvoiceDto);
            var result = await _invoiceService.CreateInvoice(invoice);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetInvoiceDetails), new
                {
                    id = invoice.InvoiceId
                }, _mapper.Map<InvoiceDto>(invoice)
                );
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<ActionResult> UpdateInvoice(UpdateInvoiceDto updateInvoiceDto)
    {
        if (ModelState.IsValid)
        {
            var invoice = _mapper.Map<Invoice>(updateInvoiceDto);
            var result = await _invoiceService.UpdateInvoice(invoice);
            if (result > 0)
            {
                return NoContent();
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteInvoice(int id)
    {
        int result = await _invoiceService.DeleteInvoice(id);
        if (result > 0)
        {
            return NoContent();
        }
        return new ObjectResult(new { error = "Internal server error" })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };


    }
}
