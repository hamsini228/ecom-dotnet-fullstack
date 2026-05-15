using Microsoft.AspNetCore.Mvc.Filters;

namespace Bajaj.eCommerce.Mvc.UI.Filters;

public class BajajActionAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"I am Action Method Filter - OnActionExecuting");
        context.HttpContext.Response.Headers.Add("X-Bajaj-Certificate", "Bosch.AbcProduct.V.10.29.90");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"I am Action Method Filter - OnActionExecuted");
    }
}
