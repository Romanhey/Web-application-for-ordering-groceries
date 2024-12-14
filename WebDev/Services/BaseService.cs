using WebDev.Models;

namespace WebDev.Services
{
    public abstract class BaseService
    {
        public ApplicationDBContext _context;
        public BaseService(ApplicationDBContext context)
        {
            _context = context;
        }
    }
}
