using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using lunch.Api.Unity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(lunch.Api.OwinStartup))]

namespace lunch.Api
{
    public class OwinStartup
    {
        private static readonly ILog Log = LogManager.GetLogger<OwinStartup>();

        public void Configuration(IAppBuilder app)
        {
            RunStartup(app);

            // Register shutdown action
            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");
            if (token != CancellationToken.None)
                token.Register(RunShutdown);
            else
                Log.Warn("Failed to register shutdown action.");

        }

        private void RunStartup(IAppBuilder app)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskException;
            LogManager.Configure();

            // Configure OWIN
            OwinConfig.Configure(app);

            // Configure WebAPI
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Log.Info("OwinStartup.RunStartup - done.");
        }

        private void RunShutdown()
        {
            UnityConfig.DisposeContainer();

            Log.Info("OwinStartup.RunShutdown - done.");
            LogManager.Shutdown();
            TaskScheduler.UnobservedTaskException -= TaskSchedulerUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomainUnhandledException;
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exceptionObject = e.ExceptionObject;
                var exception = exceptionObject as Exception;
                if (exception != null)
                    Log.Fatal("CurrentDomainUnhandledException", exception);
                else
                    Log.FatalFormat($"CurrentDomainUnhandledException: {exceptionObject}");
            }
            catch
            {
            }
        }

        private void TaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                Log.Fatal("TaskSchedulerUnobservedTaskException", e.Exception);
            }
            catch
            {
            }
        }
    }
}
