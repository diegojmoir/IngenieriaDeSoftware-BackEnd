using System.Collections.Generic;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using RestauranteAPI.Services.Injections;

namespace RestauranteAPI.Services
{
    public class TestService : ITestService
    {
        private ITestRepository _testReposiroty;
        public TestService(ITestRepository testRepository) 
        {
            _testReposiroty = testRepository;
        }
        public List<Test> GetTestDataFromFireBase()
        {
            var result = _testReposiroty.GetTestModelsAsync().Result;
            return result;
        }

        public string GetTestDocuments()
        {
            return "Ninject is working!!";
        }
    }
}
