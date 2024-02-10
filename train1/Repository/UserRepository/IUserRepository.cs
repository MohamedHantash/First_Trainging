using train1.Models;

namespace train1.Repository
{
    public interface IUserRepository
    {
        void Add(ApplicationUser user);
        void Delete(string id);
        List<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        int Save();
        void Update(ApplicationUser user);
    }
}