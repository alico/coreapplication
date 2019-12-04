using Serilog;
using Serilog.Events;
using System;

namespace CoreApplication.Bootstrapper
{
    class LogHelper
    {
        public static void ConfigureLogging()
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Warning level will exclude individual requests
               .Enrich.FromLogContext()
               //.WriteTo.Console()
               // RollingFile defaults to 31 days and a max file size of 1GB
               .WriteTo.Async(a => a.RollingFile(@"Logs\Info.log", LogEventLevel.Information)) // Good idea to wrap any file sinks in an Async sink
               .WriteTo.Async(a => a.RollingFile(@"Logs\Debug.log", LogEventLevel.Debug))
               .WriteTo.Async(a => a.RollingFile(@"Logs\Warning.log", LogEventLevel.Warning))
               .WriteTo.Async(a => a.RollingFile(@"Logs\Error.log", LogEventLevel.Error))
               .WriteTo.Async(a => a.RollingFile(@"Logs\Fatal.log", LogEventLevel.Fatal))
               .CreateLogger();

                //   //.WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/log.ndjson")
                //   //.WriteTo.Stackify()
                //   //.WriteTo.MSSqlServer(connectionString, tableName, columnOptions: columnOptions)
                //   //.WriteTo.PostgreSQL(postgreConnectionString, tableName, columnWriters, needAutoCreateTable: true)
                //   //.WriteTo.MongoDB("mongodb://127.0.0.1:27017/logs","logtest")
                //   .CreateLogger();

            }
            catch (Exception ex)
            {

            }

        }
    }
}
