
using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.CartItem;
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
public class CartItemsController : ControllerBase
{
    private readonly CartItemService _cartItemService;
    private readonly IMapper _mapper;

    public CartItemsController(CartItemService cartItemService, IMapper mapper)
    {
        _cartItemService = cartItemService;
        _mapper = mapper;
    }

    //[Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAllcartItems()
    {
        var cartItems = await _cartItemService.GetCartItemsAsync();
        if (cartItems.Count() > 0)
        {
            return Ok(_mapper.Map<IEnumerable<CartItemDto>>(cartItems));
        }
        else
        {
            return NoContent();
        }
    }

    //[Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CartItemDto>> GetcartItemDetails(int id)
    {
        var cartItem = await _cartItemService.GetCartItemDetailsAsync(id);
        if (cartItem != null)
        {
            return Ok(_mapper.Map<CartItemDto>(cartItem));
        }
        else
        {
            return NoContent();
        }
    }
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CreateCartItemDto>> CreatecartItem(CreateCartItemDto createcartItemDto)
    {
        if (ModelState.IsValid)
        {
            var cartItem = _mapper.Map<CartItem>(createcartItemDto);
            var result = await _cartItemService.CreateCartItem(cartItem);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetcartItemDetails), new
                {
                    id = cartItem.CartItemId
                }, _mapper.Map<CartItemDto>(cartItem)
                );
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<ActionResult> UpdatecartItem(UpdateCartItemDto updatecartItemDto)
    {
        if (ModelState.IsValid)
        {
            var cartItem = _mapper.Map<CartItem>(updatecartItemDto);
            var result = await _cartItemService.UpdateCartItem(cartItem);
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
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCartItem(int id)
    {
        int result = await _cartItemService.DeleteCartItem(id);
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

