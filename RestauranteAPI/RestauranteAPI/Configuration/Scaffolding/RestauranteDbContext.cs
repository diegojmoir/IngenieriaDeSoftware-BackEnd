using Microsoft.EntityFrameworkCore;
using RestauranteAPI.Models;

namespace RestauranteAPI.Configuration.Scaffolding
{
    public class RestauranteDbContext :DbContext
    {
        
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options)
        {
        }

        public DbSet<EfTest> EfTests { get; set; }

    }
}
