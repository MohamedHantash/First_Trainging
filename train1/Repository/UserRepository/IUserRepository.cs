using Microsoft.AspNetCore.Identity;
using train1.Models;

namespace train1.Repository
{
    public interface IUserRepository
    {
        void Delete(string id);
        List<ApplicationUser> GetAll();
        List<IdentityRole> GetAllRole();
        ApplicationUser GetById(string id);
        int Save();
        void Update(ApplicationUser user);
    }
}