using RestauranteAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RestauranteAPI.Repositories.Injections
{
    public interface ITestRepository
    {
        Task<List<Test>> GetTestModelsAsync();
    }
}
