using Microsoft.EntityFrameworkCore;
using ProductAPI.Model.Domain;

namespace ProductAPI.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }

        public DbSet<Catagory> catagories { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<ShoppingCart> shoppingCarts { get; set; }

    }
}
