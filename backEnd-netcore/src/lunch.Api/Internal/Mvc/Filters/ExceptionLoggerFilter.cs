using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace lunch.Api.Internal.Mvc.Filters
{
    public class ExceptionLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                var logger = context.HttpContext.RequestServices.GetService<ILogger<ExceptionLoggerFilter>>();

                var url = context.HttpContext.Request.Path;
                logger.LogError("Unhandled exception when processing '{0}': {1}", url, context.Exception);
            }
            catch
            {
                // ensure that the method noes not throw, as the excption is not handled by WebAPI
            }
        }
    }
}
