
using LabTp23.DAL;
using LabTp23.Models;
using Microsoft.EntityFrameworkCore;

namespace LabTp23Tests;

public class ShopDbContextFactory
{
    public static Guid TestProductId = Guid.NewGuid();
    public static ShopDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ShopDbContext(options);
        context.Database.EnsureCreated();
        var products = new List<Product>
        {
            new() { ID = TestProductId, Name = "Product1", Price = 10 },
            new() { Name = "Product2", Price = 20 },
            // Add more products as needed
        };

        var carts = new List<Cart>
        {
            new Cart { Products = products.Take(2).ToList() },
            // Add more carts as needed
        };

        var purchases = new List<Purchase>
        {
            new Purchase { ProductID = products[0].ID, Person = "John Doe", Address = "123 Main St", Date = DateTime.Now },
            // Add more purchases as needed
        };

        context.Product.AddRange(products);
        context.Cart.AddRange(carts);
        context.Purchase.AddRange(purchases);
        context.SaveChanges();
        return context;
    }

    public static void Destroy(ShopDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}