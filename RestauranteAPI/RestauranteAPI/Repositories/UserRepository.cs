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
                var response = client.Child("UsersCollection")
                    .Child("Users")
                     .PostAsync(user, false)
                     .Result;
                return response;
            }
        }

        public FirebaseObject<User> GetUserFromStorageByUsername(string username)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using(var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("UsersCollection")
                    .Child("Users")
                    .OnceAsync<User>()
                    .Result
                    .FirstOrDefault(x => x.Object != null && (x.Object.Username == username));
                return response;
            }
        }

        public FirebaseObject<User> GetUserFromStorageByEmail(string email)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("UsersCollection")
                    .Child("Users")
                    .OnceAsync<User>()
                    .Result
                    .FirstOrDefault(x => x.Object != null && (x.Object.Email == email));
                return response;
            }
        }

        public FirebaseObject<User> GetUserFromStorageByUserNameAndPassword(string user, string password)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("UsersCollection")
                    .Child("Users")
                    .OnceAsync<User>()
                    .Result
                    .FirstOrDefault(x => x.Object != null && (x.Object.Username == user && x.Object.Password == password));
                return response;
            }
        }
    }
}
