using Microsoft.EntityFrameworkCore;
using RestauranteAPI.Models;

namespace RestauranteAPI.Configuration.Scaffolding
{
    public class RestauranteDbContext :DbContext
    {
        
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-4KAVGK7N\\DATAANALYSIS;database=EFCoreDatabase;Trusted_Connection=True");
        }
        public DbSet<User> Users { get; set; }

    }
}
