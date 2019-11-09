using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.File;

namespace CoreApplication.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IProductEngine, ProductEngine>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddDbContext<SQLDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));
            services.AddTransient<BaseDataContext, SQLDbContext>();
        }

        public void ConfigureLogging()
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .Enrich.FromLogContext()
                   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File(@"Logs\Info-{Date}.log"))
                   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-{Date}.log"))
                   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-{Date}.log"))
                   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.File(@"Logs\Error-{Date}.log"))
                   .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-{Date}.log"))
                   .WriteTo.File(@"Logs\Verbose-{Date}.log")

                    //.WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/log.ndjson")
                   //.WriteTo.Stackify()
                   //.WriteTo.MSSqlServer(connectionString, tableName, columnOptions: columnOptions)
                   //.WriteTo.PostgreSQL(postgreConnectionString, tableName, columnWriters, needAutoCreateTable: true)
                   //.WriteTo.MongoDB("mongodb://127.0.0.1:27017/logs","logtest")
                   .CreateLogger();
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
