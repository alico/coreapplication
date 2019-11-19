using CoreApplication.DTO;
using System;
using System.Collections.Generic;

namespace CoreApplication.Business.Contracts
{
    public interface IProductEngine
    {
        ProductDTO Get(int id);
        ProductDTO Create(ProductDTO product);
        ProductDTO Edit(ProductDTO product);
        List<ProductDTO> List(int page, int size);
    }
}
