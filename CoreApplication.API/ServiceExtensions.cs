﻿using CoreApplication.Business;
using CoreApplication.Business.Contracts;
using CoreApplication.Data;
using CoreApplication.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterEngines(this IServiceCollection services)
        {
            services.AddTransient<IProductEngine, ProductEngine>();
            services.AddTransient<IBasketEngine, BasketEngine>();
            services.AddTransient<IAccountEngine, AccountEngine>();
         
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            services.AddTransient<IBasketItemRepository, BasketItemRepository>();
            return services;
        }

        public static IServiceCollection RegisterDBContext(this IServiceCollection services)
        {
            services.AddTransient<BaseDataContext, SQLDbContext>();
            return services;
        }
    }
}
