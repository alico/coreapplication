using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreApplication.DTO.RequestDTO
{
    public class AccountCreateRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Compare("Password")]
        [MinLength(5)]
        public string PasswordConfirm { get; set; }
    }
}
