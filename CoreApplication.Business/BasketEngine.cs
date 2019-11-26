using CoreApplication.Business.Contracts;
using CoreApplication.Common;
using CoreApplication.Data.Contracts;
using CoreApplication.Data.Entity;
using CoreApplication.Business.DTO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreApplication.Business
{
    public class BasketEngine : IBasketEngine
    {
        IServiceProvider _serviceProvider;

        public BasketEngine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public BasketItemDTO Get(int userId)
        {
            var basketRepository = _serviceProvider.GetService<IBasketRepository>();
            var basket = basketRepository.Get(userId);

            if (basket == null)
                throw new ItemNotFoundException();

            return new BasketItemDTO()
            {
                Id = basket.Id,
                UserId = basket.UserId
            };
        }

        public List<DetailedBaksetItemsDTO> List(int userId, int page, int size)
        {
            var basketRepository = _serviceProvider.GetService<IBasketRepository>();
            var basket = basketRepository.Get(userId);

            if (basket == null)
                throw new ItemNotFoundException();

            var basketItemRepository = _serviceProvider.GetService<IBasketItemRepository>();

            var basketItems = basketItemRepository.List(basket.Id, page, size);

            return basketItems.Select(x => new DetailedBaksetItemsDTO()
            {
                Id = x.Id,
                BasketId = x.Id,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Quantity = x.Quantity,
                Status = x.Status,
                CreationDate = x.CreationDate,
                LastModifyDate = x.LastModifydate,
            }).ToList();
        }

        public BasketItemDTO Add(BasketItemDTO basketItem)
        {
            var basketId = Get(basketItem.UserId);
            var basketItemRepository = _serviceProvider.GetService<IBasketItemRepository>();

            var entity = new BasketItem()
            {
                BasketId = basketId.Id,
                ProductId = basketItem.ProductId,
                Quantity = basketItem.Quantity,
                Status = true
            };

            basketItemRepository.Insert(entity);
            basketItem.Id = entity.Id;

            return basketItem;
        }

        public int GetBasketItemCount(int userId)
        {
            var basketRepository = _serviceProvider.GetService<IBasketRepository>();
            var basket = basketRepository.Get(userId);
            var basketItemRepository = _serviceProvider.GetService<IBasketItemRepository>();

            return basketItemRepository.Count(basket.Id);
        }
    }
}
