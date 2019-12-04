using CoreApplication.Business;
using CoreApplication.Business.Contracts;
using CoreApplication.Data;
using CoreApplication.Data.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Bootstrapper
{
    public static class DBContextServiceExtensions
    {
        public static IServiceCollection RegisterDBContext(this IServiceCollection services)
        {
            services.AddTransient<BaseDataContext, SQLDbContext>();
            return services;
        }
    }
}
