using System;

namespace ProductCatalog.Domain.Response.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
