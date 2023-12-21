using LabTp23.Models;
using Microsoft.EntityFrameworkCore;

namespace LabTp23.DAL;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Purchase> Purchase { get; set; } = null!;
    public DbSet<Cart> Cart { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasData(
                new Product { Name = "Стол", Price = 2000 },
                new Product { Name = "Стул", Price = 1000 },
                new Product { Name = "Табурет", Price = 500 });
        modelBuilder.Entity<Cart>()
            .HasData(
                new Cart { }
            );
        base.OnModelCreating(modelBuilder);
    }
}