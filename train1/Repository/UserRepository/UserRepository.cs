using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using train1.Models;
namespace train1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            this._context = context;
        }
        public List<ApplicationUser> GetAll()
        {
            return _context.Users.ToList();
        }
        public List<IdentityRole> GetAllRole()
        {
            return _context.Roles.ToList();
        }
        public ApplicationUser GetById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public void Update(ApplicationUser user)
        {
            _context.Users.Update(user);
        }
        public void Delete(string id)
        {
            _context.Users.Remove(GetById(id));
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
