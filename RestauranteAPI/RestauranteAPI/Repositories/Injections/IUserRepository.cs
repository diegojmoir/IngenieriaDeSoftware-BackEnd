using System.Threading.Tasks;
using RestauranteAPI.Models;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string user, string password);
    }
}
