using Microsoft.AspNetCore.Mvc.Filters;

namespace Bajaj.eCommerce.Mvc.UI.Filters;

public class GlobalFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("I am Global Filter - OnActionExecuted!");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("I am Global Filter - OnActionExecuting!");
    }
}
