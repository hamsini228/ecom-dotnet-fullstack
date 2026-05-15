using Microsoft.AspNetCore.Mvc.Filters;

namespace Bajaj.eCommerce.Mvc.UI.Filters;

public class BajajControllerAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("I am Controller Action Method - OnActionExecuted!");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("I am Controller Action Method - OnActionExecuting!");
    }
}
