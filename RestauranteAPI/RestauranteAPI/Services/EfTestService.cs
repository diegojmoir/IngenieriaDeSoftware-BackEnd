using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using RestauranteAPI.Services.Injections;

namespace RestauranteAPI.Services
{
    public class EfTestService:IEfTestService
    {
        public IEfTestRepository EfTestRepository;

        public EfTestService(IEfTestRepository efTestRepository)
        {

            EfTestRepository = efTestRepository;
        }

        public void SaveEfTestService(EfTest efTest)
        {
            
            EfTestRepository.SaveEfTestRecord(efTest);
        }
    }
}
