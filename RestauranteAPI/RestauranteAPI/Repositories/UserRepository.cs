using System.Collections.Generic;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using Firebase.Database;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using Firebase.Database.Query;
using System.Linq;
using RestauranteAPI.Configuration.Scaffolding;

namespace RestauranteAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected RestauranteDbContext Context { get; set; }

        public UserRepository(RestauranteDbContext context)
        {
            Context = context;
        }

        public User CreateUserInStorage(User user)
        {
            Context.Users.Add(user);
                Context.SaveChanges();
                return user;
        }

        public List<User> GetExistentUsers(string username,string email)
        {
            var result = Context.Users
                .Where(x => x.Username == username||x.Email==email)
                .ToList();//When the query is enumerated, then is sent to db
                return result;
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

        public User GetUserFromStorageByUserNameAndPassword(string user, string password)
        {
            var result=Context.Users
                .FirstOrDefault(x => x.Username == user && x.Password == password);
            return result;
        }
    }
}
