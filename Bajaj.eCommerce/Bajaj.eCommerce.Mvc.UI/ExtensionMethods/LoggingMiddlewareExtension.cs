using Bajaj.eCommerce.Mvc.UI.CustomMiddleWare;

namespace Bajaj.eCommerce.Mvc.UI.ExtensionMethods;

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
