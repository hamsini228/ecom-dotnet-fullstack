using Bajaj.eCommerce.Mvc.UI.Filters;
using Bajaj.eCommerce.Mvc.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bajaj.eCommerce.Mvc.UI.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("CustomerId", 1);
            return View();
        }
        [BajajAction]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
