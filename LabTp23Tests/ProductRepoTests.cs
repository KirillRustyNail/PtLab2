using LabTp23.DAL.Repositories.Implementations;
using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Models;

namespace LabTp23Tests;

public class ProductRepoTests : TestCommandBase
{
    private readonly IProductRepository _productRepository;

    public ProductRepoTests()
    {
        _productRepository = new ProductRepository(Context);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsListOfProducts()
    {
        // Act
        var result = await _productRepository.GetProductsAsync();

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsListOfProductsAfterAdding()
    {
        // Adding sample products to the database
        Context.Product.AddRange(
            new Product { Name = "Product1", Price = 10 },
            new Product { Name = "Product2", Price = 20 }
        );

        Context.SaveChanges();

        // Act
        var result = await _productRepository.GetProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(7, result.Count); // 2 added + 2 added on init test + 3 from context init
    }

}