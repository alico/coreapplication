using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApplication.Data.Entity
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set;}
    }
}
