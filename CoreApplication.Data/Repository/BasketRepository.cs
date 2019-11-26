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
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Basket Get(int userId)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                var entity = context.Baskets.Where(x => x.UserId == userId && x.Status).FirstOrDefault();

                if (entity == null)
                {
                    entity = new Basket()
                    {
                        UserId = userId,
                        Status = true
                    };
                    base.Insert(entity);
                }

                return entity;
            }
        }
    }
}
