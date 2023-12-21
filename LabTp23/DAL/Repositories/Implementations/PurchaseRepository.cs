using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Models;

namespace LabTp23.DAL.Repositories.Implementations;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly ShopDbContext _dbContext;

    public PurchaseRepository(
        ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Purchase purchase)
    {
        await _dbContext.Purchase.AddAsync(purchase);
        await _dbContext.SaveChangesAsync();
    }
}