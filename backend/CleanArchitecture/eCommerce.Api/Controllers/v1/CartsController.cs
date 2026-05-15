using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services;
using eCommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors("BajajPolicy")]
public class CartsController : ControllerBase
{
    private readonly CartService _cartService;
    private readonly IMapper _mapper;
    IConfiguration _configuration;
    IHttpClientFactory _httpClientFactory;

    public CartsController(CartService cartService, IMapper mapper,IHttpClientFactory httpClientFactory,IConfiguration configuration)
    {
        _cartService = cartService;
        _mapper = mapper;
        _configuration= configuration;
        _httpClientFactory = httpClientFactory;

    }

    //[Authorize(Roles = "Customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartDto>>> GetAllCarts()
    {
        var carts = await _cartService.GetCartsAsync();
        if (carts.Count() > 0)
        {
            return Ok(_mapper.Map<IEnumerable<CartDto>>(carts));
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
    public async Task<ActionResult<CartDto>> GetCartDetails(int id)
    {
        var cart = await _cartService.GetCartDetailsAsync(id);
        if (cart != null)
        {
            return Ok(_mapper.Map<CartDto>(cart));
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
    public async Task<ActionResult<CreateCartDto>> CreateCart(CreateCartDto createCartDto)
    {
        if (ModelState.IsValid)
        {
            var cart = _mapper.Map<Cart>(createCartDto);
            var result = await _cartService.CreateCart(cart);
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

    //[Authorize(Roles = "Customer")]
    public async Task<ActionResult> UpdateCart(UpdateCartDto updateCartDto)
    {
        if (ModelState.IsValid)
        {
            var cart = _mapper.Map<Cart>(updateCartDto);
            var result = await _cartService.UpdateCart(cart);
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

    //[Authorize(Roles = "Customer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCart(int id)
    {
        int result = await _cartService.DeleteCart(id);
        if (result > 0)
        {
            return NoContent();
        }
        return new ObjectResult(new { error = "Internal server error" })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }


    [AllowAnonymous]
    [HttpPost("create-razorpay-order")]
    public async Task<ActionResult> CreateRazorpayOrder(int cartId)
    {
        var keyId = _configuration["Razorpay:Key"];
        var keySecret = _configuration["Razorpay:Secret"];

        // get cart items to calculate total
        var cart = await _cartService.GetCartWithItemsAsync(cartId);
        if (cart == null)
            return BadRequest(new { message = "Cart not found." });

        if (cart.CartItems == null)
            return BadRequest(new { message = "CartItems is null." });

        if (!cart.CartItems.Any())
            return BadRequest(new { message = "Cart is empty." });

        // check if Product is loaded
        var firstItem = cart.CartItems.First();
        if (firstItem.Product == null)
            return BadRequest(new { message = "Product not loaded on cart item." });

        var amountInPaise = Convert.ToInt32(Math.Round(
            cart.CartItems.Sum(x => x.Quantity * x.Product.UnitPrice) * 100));

        // check Razorpay keys
        if (string.IsNullOrEmpty(keyId) || string.IsNullOrEmpty(keySecret))
            return BadRequest(new { message = "Razorpay keys missing from config." });

        var client = _httpClientFactory.CreateClient();
        var authToken = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{keyId}:{keySecret}"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", authToken);

        var payload = new
        {
            amount = amountInPaise,
            currency = "INR",
            receipt = Guid.NewGuid().ToString()
        };

        var response = await client.PostAsync(
            "https://api.razorpay.com/v1/orders",
            new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, new { message = responseContent });

        using var document = JsonDocument.Parse(responseContent);
        var orderId = document.RootElement.GetProperty("id").GetString();

        return Ok(new { orderId, amount = amountInPaise, currency = "INR", key = keyId });
    }

}
