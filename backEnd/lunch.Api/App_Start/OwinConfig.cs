using lunch.Api.Auth;
using Owin;

namespace lunch.Api
{
    public static class OwinConfig
    {
        public static void Configure(IAppBuilder app)
        {
            ConfigApplication(app);
            AuthConfig.Configure(app);
        }

        private static void ConfigApplication(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                //TODO: setup application context
                //context.Set(ActivationContextFactory.Create());
                var result =  next.Invoke();

                return result;
            });
        }
    }
}