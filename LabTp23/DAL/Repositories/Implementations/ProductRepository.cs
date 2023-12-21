using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Models;
using Microsoft.EntityFrameworkCore;

namespace LabTp23.DAL.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ShopDbContext _dbContext;

    public ProductRepository(
        ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Product>> GetProductsAsync()
    {
        var products = await _dbContext.Product.ToListAsync();
        return products;
    }
}