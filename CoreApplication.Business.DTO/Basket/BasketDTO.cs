using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Business.DTO
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
    }
}
