using CoreApplication.Data.Entity;
using System;
using System.Collections.Generic;

namespace CoreApplication.Data.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        List<Product> List(int page, int size);
    }
}
