using System.Web.Http.ExceptionHandling;
using log4net;

namespace lunch.Api.Errors
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog LogInstance = LogManager.GetLogger<GlobalExceptionLogger>();

        public override void Log(ExceptionLoggerContext context)
        {
            // context.Request & context.Exception are always not null
            try
            {
                var url = context.Request.RequestUri.ToString();
                LogInstance.Error($"Unhandled exception when processing '{url}'", context.Exception);
            }
            catch
            {
                // ensure that the method noes not throw, as the excption is not handled by WebAPI
            }

            base.Log(context);
        }
    }
}