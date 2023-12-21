using LabTp23.DAL.Repositories.Implementations;
using LabTp23.Models;
using LabTp23.Services.Implementations;
using LabTp23.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabTp23Tests;

public class CartServiceTests : TestCommandBase
{
    private readonly ICartService _cartService;

    public CartServiceTests()
    {
        _cartService = new CartService(
            new CartRepository(Context),
            new PurchaseRepository(Context)
        );
    }

    [Fact]
    public async Task AddProductToCart_ProductNotInCart_ReturnsTrue()
    {
        // Act
        var result = await _cartService.AddProdcutToCart(ShopDbContextFactory.TestProductId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddProductToCart_ProductAlreadyInCart_ReturnsFalse()
    {
        // Arrange
        var productId = Guid.NewGuid();
        await _cartService.AddProdcutToCart(ShopDbContextFactory.TestProductId);

        // Act
        var result = await _cartService.AddProdcutToCart(ShopDbContextFactory.TestProductId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task BuyProductsInCart_AddsPurchaseForEachProductInCart_AndClearsCart()
    {
        // Arrange
        var product1 = new Product { Name = "Product1", Price = 10 };
        var product2 = new Product { Name = "Product2", Price = 20 };

        await Context.Product.AddRangeAsync(product1, product2);
        await Context.SaveChangesAsync();

        await _cartService.AddProdcutToCart(product1.ID);
        await _cartService.AddProdcutToCart(product2.ID);

        var purchase = new Purchase
        {
            Person = "John Doe",
            Address = "123 Main St",
            Date = DateTime.Now
        };

        // Act
        await _cartService.BuyProdcutsInCart(purchase);

        // Assert
        var purchases = await Context.Purchase.ToListAsync();
        var cart = await Context.Cart.Include(c => c.Products).FirstAsync();

        Assert.Equal(3, purchases.Count); // 1 from init, 2 from test
        Assert.Empty(cart.Products);
    }
    
}