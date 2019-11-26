using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Business.Contracts;
using CoreApplication.Business.DTO;
using CoreApplication.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApplication.API.Controllers
{
    [ApiController]
    public class BasketController : BaseController
    {
        private IServiceProvider _serviceProvider;
        public BasketController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost("add")]
        public void Add(AddToBasketRequestDTO request)
        {
            var basketItem = new BasketItemDTO()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UserId = request.UserId
            };

            var basketEngine = _serviceProvider.GetService<IBasketEngine>();
            basketEngine.Add(basketItem);
        }


        [HttpGet("get")]
        public List<DetailedBaksetItemsResponseDTO> Get( int userId, int page=1, int size=10)
        {
            var basketEngine = _serviceProvider.GetService<IBasketEngine>();
            var basketItems = basketEngine.List(userId, page, size);

            return basketItems.Select(x => new DetailedBaksetItemsResponseDTO()
            {
                Id = x.Id,
                BasketId = x.Id,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                Quantity = x.Quantity,
                Status = x.Status,
                CreationDate = x.CreationDate,
                LastModifyDate = x.LastModifyDate,
            }).ToList();
        }

        [HttpGet("count")]
        public int Count(int userId)
        {
            var basketEngine = _serviceProvider.GetService<IBasketEngine>();

            return basketEngine.GetBasketItemCount(userId);
        }
    }
}