using System;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Core;

namespace lunch
{
    public static class LogManager
    {
        public static ILog GetLogger(string name)
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var logger = LoggerManager.GetLogger(callingAssembly, name);
            return new LogImpl(logger);
        }

        public static ILog GetLogger(Type type)
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var logger = LoggerManager.GetLogger(callingAssembly, type);
            return new LogImpl(logger);
        }

        public static ILog GetLogger<T>()
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var logger = LoggerManager.GetLogger(callingAssembly, typeof(T));
            return new LogImpl(logger);
        }

        public static void Configure()
        {
            XmlConfigurator.Configure();
        }

        public static void Shutdown()
        {
            LoggerManager.Shutdown();
        }
    }
}
