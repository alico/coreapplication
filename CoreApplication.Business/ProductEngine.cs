﻿using CoreApplication.Business.Contracts;
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
    public class ProductEngine : IProductEngine
    {
        IServiceProvider _serviceProvider;

        public ProductEngine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ProductDTO Get(int id)
        {
            var productRepository = _serviceProvider.GetService<IProductRepository>();
            var entity = productRepository.GetByID(id);

            if (entity == null)
                throw new ItemNotFoundException();

            return new ProductDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Quantity = entity.Quantity,
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                Status = entity.Status
            };
        }

        public ProductDTO Create(ProductDTO product)
        {
            var productRepository = _serviceProvider.GetService<IProductRepository>();

            var entity = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status,
                CreationDate = DateTime.Now,
                LastModifydate = DateTime.Now
            };

            productRepository.Insert(entity);
            product.Id = entity.Id;

            return product;
        }
        public ProductDTO Edit(ProductDTO product)
        {
            var productRepository = _serviceProvider.GetService<IProductRepository>();

            var entity = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status,
                CreationDate = DateTime.Now,
                LastModifydate = DateTime.Now
            };

            productRepository.Update(entity);
            product.Id = entity.Id;

            return product;
        }

        public List<ProductDTO> List(int page, int size)
        {
            var productRepository = _serviceProvider.GetService<IProductRepository>();
            var entities = productRepository.List(page, size);

            if (entities == null)
                throw new ItemNotFoundException();

            return entities.Select(x => new ProductDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                CategoryId = x.CategoryId,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Status = x.Status
            }).ToList();

        }
    }
}
