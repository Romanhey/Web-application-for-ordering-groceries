using WebDev.DAL.Entities;

namespace WebDev.BLL.DTO
{
    public class LoginResponse
    {
        public bool isLogged { get; set; } = false;
        public User user { get; set; }
    }
}
