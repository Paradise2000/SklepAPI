using Microsoft.EntityFrameworkCore;

namespace SklepAPI.Entities
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString =
            "Server=DESKTOP-B1V9PMR\\SQLEXPRESS;Database=SklepAPI;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrdersDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Password)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.FirstName)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.LastName)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.PhoneNumber)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.City)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Street)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.PostalCode)
               .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
