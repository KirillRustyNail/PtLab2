using LabTp23.DAL.Repositories.Implementations;
using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Models;

namespace LabTp23Tests;

public class PurchaseRepositoryTests : TestCommandBase
{
    private readonly IPurchaseRepository _purchaseRepository;

    public PurchaseRepositoryTests()
    {
        _purchaseRepository = new PurchaseRepository(Context);
    }

    [Fact]
    public async Task AddAsync_AddsPurchaseToDatabase()
    {
        // Arrange
        var purchase = new Purchase
        {
            ProductID = Guid.NewGuid(), 
            Person = "John Doe",
            Address = "123 Main St",
            Date = DateTime.Now
        };

        // Act
        await _purchaseRepository.AddAsync(purchase);

        // Assert
        var addedPurchase = Context.Purchase.FirstOrDefault(p => p.ID == purchase.ID);
        Assert.NotNull(addedPurchase);
        Assert.Equal(purchase.ProductID, addedPurchase.ProductID);
        Assert.Equal(purchase.Person, addedPurchase.Person);
        Assert.Equal(purchase.Address, addedPurchase.Address);
        Assert.Equal(purchase.Date, addedPurchase.Date);
    }
}