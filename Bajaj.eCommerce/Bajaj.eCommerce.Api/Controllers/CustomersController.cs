using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Customers;
using Bajaj.eCommerce.Api.DTOs.Invoices;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bajaj.eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICommonRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CustomersController(ICommonRepository<Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
    {
        var customers = await _customerRepository.GetAllAsync();
        if (customers.Count > 0)
        {
            return Ok(_mapper.Map<List<CustomerDto>>(customers));
        }
        else
        {
            return NoContent();
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetInvoiceDetails(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer != null)
        {
            return Ok(_mapper.Map<CustomerDto>(customer));
        }
        else
        {
            return NoContent();
        }
    }



}
