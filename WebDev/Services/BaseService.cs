using WebDev.Entities;
using WebDev.Models;

namespace WebDev.Services
{
    public abstract class BaseService
    {
        public ApplicationDbContext _context;
        public BaseService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
