using CoreApplication.Data;
using CoreApplication.Data.Contracts.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Bootstrapper
{
    public static class DBContextServiceExtension
    {
        public static IServiceCollection RegisterDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<ISQLDbContext, SQLDbContext>();
            services.AddTransient<BaseDataContext, SQLDbContext>();
            services.AddDbContext<SQLDbContext>(item => item.UseSqlServer(connectionString));

            return services;
        }
    }
}
