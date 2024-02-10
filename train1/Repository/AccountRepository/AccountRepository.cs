using Microsoft.AspNetCore.Identity;
using train1.Models;
namespace train1.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;
        public AccountRepository(ApplicationContext context)
        {
            this._context = context;
        }
        public List<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }

    }
}
