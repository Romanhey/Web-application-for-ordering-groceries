using WebDev.DAL.Entities;

namespace WebDev.BLL.Services
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
