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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("DefaultPolicy");
            loggerFactory.AddSerilog();

            LogHelper.ConfigureLogging();

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
