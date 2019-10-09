using System.Threading.Tasks;
using RestauranteAPI.Models;
using Firebase.Database;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IUserRepository
    {
         FirebaseObject<User> GetUserFromStorageByUserNameAndPassword(string user, string password);
         FirebaseObject<User> CreateUserInStorage(User user);

        FirebaseObject<User> GetUserFromStorageByUsername(string username);

        FirebaseObject<User> GetUserFromStorageByEmail(string email);
    }
}
