using CoreApplication.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.File;
using Serilog.Sinks.Async;
using CoreApplication.Bootstrapper;

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
            var connectionString = Configuration.GetConnectionString("PostgreSQLConnection");

            //Swagger
            services.RegisterSwager();
            services.AddControllers();
            services.RegisterDBContext(connectionString);
            services.RegisterRepositories();
            services.RegisterEngines();
            services.RegisterCorsPolicy();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("DefaultPolicy");
            app.UseHttpsRedirection();

            //Swagger
            app.UseSwaggerConfiguration();
            loggerFactory.AddSerilog();

            LogHelper.ConfigureLogging();

            app.UseMiddleware(typeof(ResponseWrapper));
            app.UseMiddleware(typeof(LoggingMiddleware));
            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseRouting();

            //Creation DB - CodeFirst
            app.EnsureDBCreated();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
