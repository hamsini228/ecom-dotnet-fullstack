using Bajaj.eCommerce.Dal;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bajaj.eCommerce.Mvc.UI.Areas.Invoices.Controllers;

[Area("Invoices")]
public class HomeController : Controller
{
    private readonly ICartRepository _cartRepository;
    private readonly eCommerceDbContext _context; 

    public HomeController(ICartRepository cartRepository, eCommerceDbContext context)
    {
        _cartRepository = cartRepository;
        _context = context;
    }

    // Called from Success page — creates invoice and redirects to Details
    public async Task<IActionResult> Create(string paymentId, string orderId)
    {
        var cartId = HttpContext.Session.GetInt32("CartId") ?? 0;
        var items = await _cartRepository.GetYourCartItemsAsync(cartId);

        if (!items.Any())
            return RedirectToAction("Index", "Home", new { area = "Products" });

        // Create Invoice
        var invoice = new Invoice
        {
            InvoiceDate = DateTime.Now,
            CartId = cartId,
            PaymentId = paymentId,
            OrderId = orderId,
            InvoiceItems = items.Select(x => new InvoiceItem
            {
                ProductName = x.ProductName,
                CategoryName = x.CategoryName,
                Quantity = x.Quantity,
                Price = x.Price,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice
            }).ToList()
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        HttpContext.Session.Remove("CartId");

        return RedirectToAction("Details", new { id = invoice.InvoiceId });
    }

    // Displays the invoice
    public async Task<IActionResult> Details(int id)
    {
        var invoice = await _context.Invoices
            .Include(x => x.InvoiceItems)
            .FirstOrDefaultAsync(x => x.InvoiceId == id);

        if (invoice == null)
            return NotFound();

        var vm = new InvoiceVM
        {
            InvoiceId = invoice.InvoiceId,
            InvoiceDate = invoice.InvoiceDate,
            PaymentId = invoice.PaymentId,
            OrderId = invoice.OrderId,
            Items = invoice.InvoiceItems.ToList()
        };

        return View(vm);
    }
}