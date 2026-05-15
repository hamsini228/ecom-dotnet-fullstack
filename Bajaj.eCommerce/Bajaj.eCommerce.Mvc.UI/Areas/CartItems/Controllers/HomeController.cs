using Microsoft.AspNetCore.Mvc;

namespace Bajaj.eCommerce.Mvc.UI.Areas.CartItems.Controllers;
[Area("CartItems")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
