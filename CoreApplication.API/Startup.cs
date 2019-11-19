using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Business;
using CoreApplication.Business.Contracts;
using CoreApplication.Data;
using CoreApplication.Data.Contracts;
using CoreApplication.Data.Contracts.Context;
using CoreApplication.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.File;
using Serilog.Sinks.Async;

namespace CoreApplication.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IProductEngine, ProductEngine>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddDbContext<SQLDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));
            services.AddTransient<BaseDataContext, SQLDbContext>();

            services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        public void ConfigureLogging()
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

                //Log.Logger = new LoggerConfiguration()
                //   .MinimumLevel.Error()
                //   .Enrich.FromLogContext()
                //   .WriteTo.Async(l => l..Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File(@"Logs\Info-{Date}.log"))
                //   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-{Date}.log"))
                //   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-{Date}.log"))
                //   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.File(@"Logs\Error-{Date}.log"))
                //   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-{Date}.log"))
                //   .WriteTo.File(@"Logs\Verbose-{Date}.log")

                //   //.WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/log.ndjson")
                //   //.WriteTo.Stackify()
                //   //.WriteTo.MSSqlServer(connectionString, tableName, columnOptions: columnOptions)
                //   //.WriteTo.PostgreSQL(postgreConnectionString, tableName, columnWriters, needAutoCreateTable: true)
                //   //.WriteTo.MongoDB("mongodb://127.0.0.1:27017/logs","logtest")
                //   .CreateLogger();

                //Log.Logger = new LoggerConfiguration()
                //.WriteTo.Async(a => a.File(@"Logs\CustomLog.log")).MinimumLevel.Error()
                //// Other logger configuration
                //.CreateLogger();
            }
            catch (Exception ex)
            {

            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("DefaultPolicy");
            loggerFactory.AddSerilog();

            ConfigureLogging();

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ResponseWrapper));
            app.UseMiddleware(typeof(LoggingMiddleware));
            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseRouting();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SQLDbContext>();
                context.Database.EnsureCreated();
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
