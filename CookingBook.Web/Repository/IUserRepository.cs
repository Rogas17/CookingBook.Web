using Microsoft.AspNetCore.Identity;

namespace CookingBook.Web.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
