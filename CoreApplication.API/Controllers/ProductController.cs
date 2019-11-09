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

namespace CoreApplication.API.Controllers
{
    public class ProductController : BaseController
    {
        IProductEngine _productEngine;

        public ProductController(IServiceProvider serviceProvider, ILogger<ProductController> logger) : base(logger)
        {
            _productEngine = serviceProvider.GetService<IProductEngine>();
        }

        [HttpGet]
        [HttpGet("all")]
        public IList<ProductDTO> All(int page = 1, int size = 10)
        {
            return _productEngine.List(page, size);
        }

        [HttpGet("get")]
        public ProductDTO Get(int id)
        {
            return _productEngine.Get(id);
        }

        [HttpPost("create")]
        public ProductDTO Create(ProductDTO product)
        {
            return _productEngine.Put(product);
        }
    }
}