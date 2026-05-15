using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Carts;
using Bajaj.eCommerce.Api.DTOs.Products;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Bajaj.eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICommonRepository<Cart> _cartRepository;
    private readonly ICartRepository _customerCartRepository;
    private readonly IMapper _mapper;

    public CartsController(ICommonRepository<Cart> cartRepository, IMapper mapper,ICartRepository customerCartRepository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _customerCartRepository = customerCartRepository;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<List<CartDto>>> GetAllCarts()
    {
        var carts = await _cartRepository.GetAllAsync();
        if (carts.Count > 0)
        {
            return Ok(_mapper.Map<List<CartDto>>(carts));
        }
        else
        {
            return NoContent();
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CartDto>> GetCartDetails(int id)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        if (cart != null)
        {
            return Ok(_mapper.Map<CartDto>(cart));
        }
        else
        {
            return NoContent();
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("customer/{customerId:int}")]
    public async Task<ActionResult<List<CartDto>>> GetCartsByCustomerID(int customerId)
    {
        //var carts = await _customerCartRepository.GetCartsByCustomerIdAsync(customerId);
        var cartsold =await _cartRepository.GetAllAsync();
        var carts =cartsold.Where(c=>c.CustomerId==customerId).ToList();

        if (carts.Count>0)
            return Ok(_mapper.Map<List<CartDto>>(carts));
        else
            return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult> CreateCart(CreateCartDto createCartDto)
    {
        if (ModelState.IsValid)
        {
            var cart = _mapper.Map<Cart>(createCartDto);
            var result = await _cartRepository.AddAsync(cart);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetCartDetails), new
                {
                    id = cart.CartId
                }, _mapper.Map<CartDto>(cart)
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
    public async Task<ActionResult> UpdateCart(UpdateCartDto updateCartDto)
    {
        if (ModelState.IsValid)
        {
            var cart = _mapper.Map<Cart>(updateCartDto);
            var result = await _cartRepository.UpdateAsync(cart);
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
    public async Task<ActionResult> DeleteCart(int id)
    {
        int result = await _cartRepository.DeleteAsync(id);
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
