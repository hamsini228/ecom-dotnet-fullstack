namespace Bajaj.eCommerce.Mvc.UI.CustomMiddleWare;

public class MyCustomMiddleware
{
    private readonly RequestDelegate _next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Before next middleware
        Console.WriteLine("Request incoming");

        await _next(context);

        // After next middleware
        Console.WriteLine("Response outgoing");
    }
}
