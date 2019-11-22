﻿using CoreApplication.Data.Contracts;
using CoreApplication.Data.Contracts.Context;
using CoreApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;

namespace CoreApplication.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BaseDataContext context ) : base(context)
        {
        }

        public List<Product> List(int page, int size)
        {
            return context.Products.Skip((page - 1) * size).Take(size).ToListWithNoLock();
        }

    }
}
