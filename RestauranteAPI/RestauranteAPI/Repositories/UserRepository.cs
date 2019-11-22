using System.Collections.Generic;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
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

        public User GetUserFromStorageByEmail(string email)
        {

            var result = Context.Users
                .FirstOrDefault(x => x.Email==email);//When the query is enumerated, then is sent to db
            return result;

          
        }

        public User GetUserFromStorageByUserNameAndPassword(string user, string password)
        {
            var result=Context.Users
                .FirstOrDefault(x => x.Username == user && x.Password == password);
            return result;
        }
    }
}
