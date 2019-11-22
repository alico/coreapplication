using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreApplication.DTO.RequestDTO
{
    public class CreateProductRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [MinLength(5)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public IFormFile File { get; set; }

    }
}
