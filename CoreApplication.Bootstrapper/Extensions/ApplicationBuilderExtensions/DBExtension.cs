using CoreApplication.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Bootstrapper
{
    public static class DBExtension
    {
        public static IApplicationBuilder EnsureDBCreated(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<BaseDataContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception)
                {

                    throw new Exception("DB server was not found! Check the connectionstring.");
                }

            }

            return app;
        }
    }
}
