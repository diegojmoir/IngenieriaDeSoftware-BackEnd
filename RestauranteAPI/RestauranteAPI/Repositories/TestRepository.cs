using RestauranteAPI.Configuration.FirebaseConfiguration;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using System.Collections.Generic;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace RestauranteAPI.Repositories
{
    public class TestRepository : ITestRepository
    {
        public async Task<List<Test>> GetTestModelsAsync()
        {
            await FirebaseConfig.FirebaseStartUp();
            using (var client=FirebaseConfig.FirebaseClient) 
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
                return result;     
            }
                
        }
    }
}
