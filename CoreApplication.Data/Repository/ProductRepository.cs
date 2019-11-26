using CoreApplication.Data.Contracts;
using CoreApplication.Data.Contracts.Context;
using CoreApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApplication.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<Product> List(int page, int size)
        {
            using (new TransactionScope(
                 TransactionScopeOption.Required,
                 new TransactionOptions
                 {
                     IsolationLevel = IsolationLevel.ReadUncommitted
                 }))
            {
                using (var context = _serviceProvider.GetService<BaseDataContext>())
                {
                    return context.Products.Skip((page - 1) * size).Take(size).ToList();
                }
            }
        }

    }
}
