using Microsoft.AspNetCore.Identity;

namespace train1.Repository
{
    public interface IAccountRepository
    {
        List<IdentityRole> GetRoles();
    }
}