using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Models;
using Microsoft.EntityFrameworkCore;

namespace LabTp23.DAL.Repositories.Implementations;

public class CartRepository : ICartRepository
{
    private readonly ShopDbContext _dbContext;

    public CartRepository(
        ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Guid productId)
    {
        var cart = await GetAsync();
        var product = await _dbContext.Product.FindAsync(productId);
        cart.Products.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Clear()
    {
        var cart = await GetAsync();
        cart.Products.Clear();
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Cart> GetAsync()
    {
        var cart = await _dbContext.Cart.Include(x=> x.Products).FirstAsync();
        return cart;
    }
}