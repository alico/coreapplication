using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApplication.Data.Entity
{
    [Table("Basket")]
    public class Basket : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
    }
}
