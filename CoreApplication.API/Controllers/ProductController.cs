using System;
using System.Collections.Generic;
using CoreApplication.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreApplication.DTO.RequestDTO;
using CoreApplication.Common;
using CoreApplication.Business.DTO;

namespace CoreApplication.API.Controllers
{
    public class ProductController : BaseController
    {
        private IServiceProvider _serviceProvider;

        public ProductController(IServiceProvider serviceProvider, ILogger<ProductController> logger) : base(logger)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        [HttpGet("all")]
        public IList<ProductDTO> All(int page = 1, int size = 10)
        {
            var productEngine = _serviceProvider.GetService<IProductEngine>();
            return productEngine.List(page, size);
        }

        [HttpGet("get")]
        public ProductDTO Get(int id)
        {
            var productEngine = _serviceProvider.GetService<IProductEngine>();
            return productEngine.Get(id);
        }

        [HttpPost("create")]
        public ProductDTO Create([FromForm]CreateProductRequestDTO collection)
        {
            var productEngine = _serviceProvider.GetService<IProductEngine>();
            var imageUrl = FileHelper.UploadFile(collection.File);

            var product = new ProductDTO()
            {
                CategoryId = collection.CategoryId,
                Description = collection.Description,
                ImageUrl = imageUrl,
                Name = collection.Name,
                Price = collection.Price,
                Quantity = collection.Quantity,
                Status = collection.Status
            };

            return productEngine.Create(product);
        }

        [HttpPost("edit")]
        public ProductDTO Edit([FromForm]EditProductRequestDTO collection)
        {
            var productEngine = _serviceProvider.GetService<IProductEngine>();
            var imageUrl = FileHelper.UploadFile(collection.File);

            var product = new ProductDTO()
            {
                Id = collection.Id,
                CategoryId = collection.CategoryId,
                Description = collection.Description,
                ImageUrl = imageUrl,
                Name = collection.Name,
                Price = collection.Price,
                Quantity = collection.Quantity,
                Status = collection.Status
            };

            return productEngine.Edit(product);
        }
    }


}