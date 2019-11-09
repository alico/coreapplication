using CoreApplication.Data.Contracts;
using CoreApplication.Data.Contracts.Context;
using CoreApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CoreApplication.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private BaseDataContext _context;
        public ProductRepository(BaseDataContext context ) : base(context)
        {
            _context = context;
        }

        public List<Product> List(int page, int size)
        {
            return _context.Products.Skip((page - 1) * size).Take(size).ToList();
        }

    }
}
