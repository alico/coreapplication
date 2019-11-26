using CoreApplication.Business.DTO;
using System;
using System.Collections.Generic;

namespace CoreApplication.Business.Contracts
{
    public interface IBasketEngine
    {
        BasketItemDTO Get(int id);
        BasketItemDTO Add(BasketItemDTO basketItem);
        List<DetailedBaksetItemsDTO> List(int userId, int page, int size);
        int GetBasketItemCount(int userId);
    }
}
