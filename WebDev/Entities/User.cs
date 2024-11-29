using Microsoft.AspNetCore.Identity;

namespace WebDev.Models
{
    public class User
    {
        // Публичные свойства
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public bool isAdmin { get; set; }

        public List<Order> Orders { get; set; }
    }
}
