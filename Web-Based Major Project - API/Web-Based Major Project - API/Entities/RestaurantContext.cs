using Microsoft.EntityFrameworkCore;

namespace Web_Based_Major_Project___API.Entities
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions option): base(option) { }

        public DbSet<Product> Products { get; set; }
    }
}
