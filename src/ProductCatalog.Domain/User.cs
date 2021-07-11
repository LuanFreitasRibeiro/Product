using System;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } 
    }
}
