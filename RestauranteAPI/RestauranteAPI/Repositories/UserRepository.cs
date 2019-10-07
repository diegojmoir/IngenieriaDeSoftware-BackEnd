using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestauranteAPI.Models;
using Firebase.Database.Query;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using RestauranteAPI.Repositories.Injections;

namespace RestauranteAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserAsync(string user, string password)
        {
            await FirebaseConfig.FirebaseStartUp();
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var data = await client.Child("tests")
                    .OrderByKey()
                          .OnceAsync<Test>();
                var result = new List<Test>();
                foreach (var testObject in data)
                {
                    var test = new Test
                    {
                        Key = testObject.Key,
                        TestName = testObject.Object.TestName
                    };
                    result.Add(test);
                }
                return null;
            }
        }
    }
}
