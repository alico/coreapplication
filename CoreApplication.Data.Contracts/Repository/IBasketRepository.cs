using CoreApplication.Data.Entity;
using System;
using System.Collections.Generic;

namespace CoreApplication.Data.Contracts
{
    public interface IBasketRepository : IRepositoryBase<Basket>
    {
        Basket Get (int userId);
    }
}
