using System.Collections.Generic;
using RestauranteAPI.Models;
using Firebase.Database;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IUserRepository
    {
         User GetUserFromStorageByUserNameAndPassword(string user, string password);
         User CreateUserInStorage(User user);
         List<User> GetExistentUsers(string username,string email);
         FirebaseObject<User> GetUserFromStorageByEmail(string email);
    }
}
