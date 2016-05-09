using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(lunch.Api.OwinStartup))]

namespace lunch.Api
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            OwinConfig.Configure(app);
        }
    }
}
