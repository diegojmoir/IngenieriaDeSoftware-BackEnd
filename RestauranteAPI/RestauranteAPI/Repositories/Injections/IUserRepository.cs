using System.Threading.Tasks;
using RestauranteAPI.Models;
using Firebase.Database;
using System.IdentityModel.Tokens.Jwt;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IUserRepository
    {
         FirebaseObject<User> GetUserFromStorageByUserNameAndPassword(string user, string password);
         FirebaseObject<User> CreateUserInStorage(User user);

        FirebaseObject<User> GetUserFromStorageByUsername(string username);

        FirebaseObject<User> GetUserFromStorageByEmail(string email);

        string CreateToken(string username);
    }
}
