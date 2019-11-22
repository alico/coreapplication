using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.DTO
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
    }
}
