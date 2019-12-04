using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CoreApplication.Bootstrapper
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection RegisterSwager(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreApplication API", Version = "v1" });
            });

            return services;
        }
    }
}
