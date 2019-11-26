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
    public class BasketItemRepository : BaseRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<DetailedBaksetItems> List(int basketId, int page, int size)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                var result = context.BasketItems.Join(context.Products,
                                 basket => basket.ProductId,
                                 product => product.Id,
                                (basket, product) => new { basket, product })
                    .Where(x => x.basket.BasketId == basketId)
                    .Skip((page - 1) * size).Take(size)
                    .Select(x => new DetailedBaksetItems()
                    {
                        Id = x.basket.Id,
                        BasketId = x.basket.Id,
                        ProductId = x.product.Id,
                        ProductName = x.product.Name,
                        ImageUrl = x.product.ImageUrl,
                        Quantity = x.basket.Quantity,
                        Price = x.product.Price,
                        Status = x.basket.Status,
                        CreationDate = x.basket.CreationDate,
                        LastModifydate = x.basket.LastModifydate,
                    }).AsNoTracking().ToList();

                return result;
            }
        }

        public BasketItem Get(int basketId, int productId)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                return context.BasketItems.Where(x => x.BasketId == basketId && x.ProductId == productId).FirstOrDefault();
            }
        }

        public override void Insert(BasketItem entity)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                var oldEntity = Get(entity.BasketId, entity.ProductId);

                if (oldEntity == null)
                    base.Insert(entity);
                else
                {
                    oldEntity.Quantity = entity.Quantity;
                    context.Update(oldEntity);
                    context.SaveChanges();
                }
            }
        }

        public int Count(int basketId)
        {
            using (var context = _serviceProvider.GetService<BaseDataContext>())
            {
                return context.BasketItems.Where(x => x.BasketId == basketId).Count();
            }
        }
    }
}
