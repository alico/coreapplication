using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using CoreApplication.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CoreApplication.DTO.RequestDTO.Product;
using CoreApplication.Common;

namespace CoreApplication.API.Controllers
{
    public class ProductController : BaseController
    {
        IProductEngine _productEngine;
   

        private IHostingEnvironment _hostingEnvironment;

        public ProductController(IServiceProvider serviceProvider, ILogger<ProductController> logger, IHostingEnvironment environment) : base(logger)
        {
            _productEngine = serviceProvider.GetService<IProductEngine>();
            _hostingEnvironment = environment;
           
        }

        [HttpGet]
        [HttpGet("all")]
        public IList<ProductDTO> All(int page = 1, int size = 10)
        {
            //base._logger.LogInformation("test");
            return _productEngine.List(page, size);
        }

        [HttpGet("get")]
        public ProductDTO Get(int id)
        {
            return _productEngine.Get(id);
        }

        [HttpPost("create")]
        public ProductDTO Create([FromForm]CreateProductRequestDTO collection)
        {
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

            return _productEngine.Create(product);
        }

        [HttpPost("edit")]
        public ProductDTO Edit([FromForm]EditProductRequestDTO collection)
        {
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

            return _productEngine.Edit(product);
        }
    }


}