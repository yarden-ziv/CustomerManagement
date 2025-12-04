using CustomerManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "ירושלים" },
                new City { Id = 2, Name = "תל אביב" },
                new City { Id = 3, Name = "חיפה" },
                new City { Id = 4, Name = "באר שבע" },
                new City { Id = 5, Name = "אשדוד" }
            );
        }
    }
}
