using lunch.Api.Auth;
using Owin;

namespace lunch.Api
{
    public static class OwinConfig
    {
        public static void Configure(IAppBuilder app)
        {
            AuthConfig.Configure(app);

            ConfigApplication(app);
        }

        private static void ConfigApplication(IAppBuilder app)
        {
            //app.Use((context, next) =>
            //{
            //    var result =  next.Invoke();
            //    return result;
            //});
        }
    }
}