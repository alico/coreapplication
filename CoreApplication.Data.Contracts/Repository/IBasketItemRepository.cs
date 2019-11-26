using CoreApplication.Data.Entity;
using System;
using System.Collections.Generic;

namespace CoreApplication.Data.Contracts
{
    public interface IBasketItemRepository : IRepositoryBase<BasketItem>
    {
        List<DetailedBaksetItems> List(int basketId, int page, int size);
        int Count(int basketId);
    }
}
