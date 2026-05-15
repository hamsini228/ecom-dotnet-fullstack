using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Customer;
using eCommerce.Application.Services;
using eCommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors("BajajPolicy")]
public class CustomersController(CustomerService _customerService , IMapper _mapper) : ControllerBase
{

    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetallAsync()
    {
        var customers = await _customerService.GetCustomersAsync();
        if (customers.Count() > 0)
        {
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
        }
        return NoContent();
    }
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var customer = await _customerService.GetCustomerDetailsAsync(id);
        if (customer != null)
        {
            return Ok(_mapper.Map<CustomerDto>(customer));
        }
        return NoContent();
    }

    [HttpGet("user/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> GetByUserId(int userId)
    {
        var customer = await _customerService.GetCustomerByUserIdAsync(userId);
        if (customer != null)
            return Ok(_mapper.Map<CustomerDto>(customer));

        return NotFound();
    }

    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[HttpPost]
    //public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto createCustomerDto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var customer = _mapper.Map<Customer>(createCustomerDto);
    //        var result = await _customerService.CreateCustomer(customer);
    //        if (result > 0)
    //        {
    //            return CreatedAtAction(nameof(GetById), new
    //            {
    //                id = customer.CustomerId,
    //            }, _mapper.Map<CustomerDto>(customer)
    //            );
    //        }
    //        return new ObjectResult(new { error = "Internal server error" })
    //        {
    //            StatusCode = StatusCodes.Status500InternalServerError
    //        };
    //    }
    //    return BadRequest();
    //}
    ////[Authorize(Roles = "Admin,Customer")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[HttpPut]
    //public async Task<ActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var customer = _mapper.Map<Customer>(updateCustomerDto);
    //        var result = await _customerService.UpdateCustomer(customer);
    //        if (result > 0)
    //        {
    //            return NoContent();
    //        }
    //        return new ObjectResult(new { error = "Internal server error" })
    //        {
    //            StatusCode = StatusCodes.Status500InternalServerError
    //        };
    //    }
    //    return BadRequest();
    //}
    ////[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[HttpDelete("{id:int}")]
    //public async Task<ActionResult> DeleteCustomer(int id)
    //{
    //    int result = await _customerService.DeleteCustomer(id);

    //    if (result > 0)
    //    {
    //        return NoContent();
    //    }
    //    return new ObjectResult(new { error = "Internal server error" })
    //    {
    //        StatusCode = StatusCodes.Status500InternalServerError
    //    };

    //}

}
