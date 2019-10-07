using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using Firebase.Database;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using Firebase.Database.Query;
using System.Linq;
namespace RestauranteAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public FirebaseObject<User> CreateUserInStorage(User user)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var response = client.Child("Users")
                     .PostAsync(user, false)
                     .Result;
                return response;
            }
        }

        public FirebaseObject<User> GetUserFromStorageByUserNameAndPassword(string user, string password)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("Users")
                    .OnceAsync<User>()
                    .Result
                    .FirstOrDefault(x => x.Object != null && (x.Object.Username == user && x.Object.Password == password));
                return response;
            }
        }
    }
}
