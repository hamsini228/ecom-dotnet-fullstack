using Serilog;

namespace Bajaj.eCommerce.Mvc.UI.CustomMiddleWare;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Before next middleware
        Console.WriteLine("Request Exception MIddle wareincoming");
        try
        {
            await _next(context);
        }
        catch (Exception ex) {
            //Log.Error(ex, "Error Occured");

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.ToString());
        }
        

        // After next middleware
        Console.WriteLine("Response Exception middleware outgoing");
    }
}
