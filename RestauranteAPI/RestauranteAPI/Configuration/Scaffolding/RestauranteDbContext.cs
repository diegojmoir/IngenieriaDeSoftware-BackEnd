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
            optionsBuilder.UseSqlServer("server=restaurante-dev.ctthpo6dykjb.us-east-2.rds.amazonaws.com;database=dev_restaurante;User Id=admin; Password=password");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}
