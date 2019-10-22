using RestauranteAPI.Configuration.Scaffolding;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;

namespace RestauranteAPI.Repositories
{
    public class EfTestRepository : IEfTestRepository
    {
        private readonly RestauranteDbContext _context;

        public EfTestRepository(RestauranteDbContext context)
        {
            _context = context;
        }
        public void SaveEfTestRecord(EfTest efTest)
        {
            _context.EfTests.Add(efTest);
            _context.SaveChanges();
        }
    }
}
