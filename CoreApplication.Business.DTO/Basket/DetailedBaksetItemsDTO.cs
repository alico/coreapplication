using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Business.DTO
{
    public class DetailedBaksetItemsDTO
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifyDate { get; set; }

    }
}
