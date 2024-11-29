using WebDev.Models;

namespace WebDev.DTO
{
    public class LoginResponse
    {
        public bool isLogged {  get; set; }=false;
        public User user { get; set; }
    }
}
