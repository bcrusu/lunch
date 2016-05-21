using System;

namespace lunch
{
    public static class LogManager
    {
        public static ILog GetLogger(string name)
        {
            throw new NotImplementedException();
        }

        public static ILog GetLogger(Type type)
        {
            throw new NotImplementedException();
        }

        public static ILog GetLogger<T>()
        {
            throw new NotImplementedException();
        }

        public static void Configure()
        {
            throw new NotImplementedException();
        }

        public static void Shutdown()
        {
            throw new NotImplementedException();
        }
    }

    public interface ILog
    {
    }
}
