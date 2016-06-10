using System.Diagnostics;
using log4net;
using lunch.Api.Auth;
using Owin;

namespace lunch.Api
{
    public static class OwinConfig
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(OwinConfig));

        public static void Configure(IAppBuilder app)
        {
            ConfigDebugMiddleware(app);
            AuthConfig.Configure(app);
        }

        [Conditional("DEBUG")]
        private static void ConfigDebugMiddleware(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                var uri = context.Request.Uri;
                var method = context.Request.Method;

                var result = next.Invoke();
                return result;
            });
        }
    }
}