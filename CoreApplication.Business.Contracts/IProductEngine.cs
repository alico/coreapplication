using CoreApplication.DTO;
using System;
using System.Collections.Generic;

namespace CoreApplication.Business.Contracts
{
    public interface IProductEngine
    {
        ProductDTO Get(int id);
        ProductDTO Put(ProductDTO product);
        List<ProductDTO> List(int page, int size);
    }
}
