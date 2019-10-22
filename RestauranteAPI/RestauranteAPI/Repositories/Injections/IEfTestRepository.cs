using RestauranteAPI.Models;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IEfTestRepository
    {
        void SaveEfTestRecord(EfTest efTest);
    }
}
