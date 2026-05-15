using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Bajaj.eCommerce.Mvc.UI.Areas.Payments.Controllers;

[Area("Payments")]
public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICartRepository _cartRepository;

    public HomeController(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        ICartRepository cartRepository)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _cartRepository = cartRepository;
    }

    // Shows the payment page with order summary
    public async Task<IActionResult> Index()
    {
        var cartId = HttpContext.Session.GetInt32("CartId") ?? 0;
        var items = await _cartRepository.GetYourCartItemsAsync(cartId);

        if (cartId <= 0 || !items.Any())
            return RedirectToAction("YourCart", "Home", new { area = "Carts" });

        ViewBag.RazorpayKeyId = _configuration["Razorpay:Key"];

        return View(items);
    }

    // Called via AJAX to create Razorpay order
    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> CreateRazorpayOrder()
    {
        var keyId = _configuration["Razorpay:Key"];
        var keySecret = _configuration["Razorpay:Secret"];
        var currency = "INR";

        var cartId = HttpContext.Session.GetInt32("CartId") ?? 0;
        var items = await _cartRepository.GetYourCartItemsAsync(cartId);

        if (cartId <= 0 || !items.Any())
            return BadRequest(new { message = "Cart is empty." });

        var amountInPaise = Convert.ToInt32(Math.Round(items.Sum(x => x.TotalPrice) * 100));

        var client = _httpClientFactory.CreateClient();
        var authToken = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{keyId}:{keySecret}"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", authToken);

        var payload = new
        {
            amount = amountInPaise,
            currency,
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

        return Ok(new { orderId, amount = amountInPaise, currency, key = keyId });
    }

    // Success page after payment
    public IActionResult Success(string paymentId, string orderId)
    {
        var cartId = HttpContext.Session.GetInt32("CartId") ?? 0;
        ViewBag.PaymentId = paymentId;
        ViewBag.OrderId = orderId;
        ViewBag.CartId = cartId;
        return View();
    }
}