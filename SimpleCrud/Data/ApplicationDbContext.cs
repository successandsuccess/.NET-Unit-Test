// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using SimpleCrud.Models;  // Make sure this matches your namespace

namespace SimpleCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SimpleCrud.Models.Product> Product { get; set; } = default!;

        // Add your DbSets here
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // Define precision and scale
        }
    }
}