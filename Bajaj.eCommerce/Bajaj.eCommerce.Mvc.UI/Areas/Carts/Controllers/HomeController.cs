using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Mvc.UI.Filters;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Razorpay.Api;
using Razorpay.Api;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Bajaj.eCommerce.Mvc.UI.Areas.Carts.Controllers;
[Area("Carts")]
public class HomeController : Controller
{
    private readonly ICommonRepository<Cart> _cartRepository;
    private readonly ICommonRepository<CartItem> _cartItemRepository;
    private readonly ICartRepository _myCartRepository;
    private readonly IConfiguration _configuration;
    public HomeController(ICommonRepository<Cart> cartRepository, ICommonRepository<CartItem> cartItemRepository, 
        ICartRepository myCartRepository, IConfiguration configuration)
    {
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _myCartRepository = myCartRepository;
        _configuration = configuration;
    }
    public async Task<IActionResult> YourCart()
    {
        var items = await _myCartRepository.GetYourCartItemsAsync(HttpContext.Session.GetInt32("CartId") ?? 0);
        return View(items);
    }
    public async Task<IActionResult> AddToCart(int productId)
    {
        if (HttpContext.Session.GetInt32("CartId") == null || HttpContext.Session.GetInt32("CartId") <= 0)
        {
            var cart = new Cart
            {
                CustomerId = HttpContext.Session.GetInt32("CustomerId") ?? 0,
                CartDate = DateTime.Now
            };
            int result = await _cartRepository.AddAsync(cart);
            if (result > 0)
            {
                HttpContext.Session.SetInt32("CartId", cart.CartId);
            }
        }
        var cartItem = new CartItem
        {
            CartId = HttpContext.Session.GetInt32("CartId") ?? 0,
            ProductId = productId,
            Size = 7,
            Quantity = 1
        };
        await _cartItemRepository.AddAsync(cartItem);

        return RedirectToAction("YourCart");
    }
    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int cartItemId)
    {
        var item = await _cartItemRepository.GetByIdAsync(cartItemId);

        if (item != null)
        {
            await _myCartRepository.RemoveCartItemAsync(item.CartItemId);
        }

        return RedirectToAction("YourCart");
    }
    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int cartItemId, string actionType)
    {
        var item = await _cartItemRepository.GetByIdAsync(cartItemId);

        if (item != null)
        {
            if (actionType == "increase")
            {
                item.Quantity += 1;
            }
            else if (actionType == "decrease")
            {
                item.Quantity -= 1;

                if (item.Quantity <= 0)
                {
                    await _cartItemRepository.DeleteAsync(item.CartItemId);
                    return RedirectToAction("YourCart");
                }
            }

            await _cartItemRepository.UpdateAsync(item);
        }

        return RedirectToAction("YourCart");
    }

    
    
   
}