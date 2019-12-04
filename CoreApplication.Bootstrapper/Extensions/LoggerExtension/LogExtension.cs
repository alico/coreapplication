using Microsoft.Extensions.Logging;
using Serilog;

namespace CoreApplication.Bootstrapper
{
    public static class LogExtension
    {
        public static ILoggerFactory UseLogger(this ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            LogHelper.ConfigureLogging();

            return loggerFactory;
        }
    }
}
