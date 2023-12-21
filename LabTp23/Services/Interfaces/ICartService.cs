using LabTp23.Models;

namespace LabTp23.Services.Interfaces;

public interface ICartService
{
    public Task<bool> AddProdcutToCart(Guid productId);
    public Task BuyProdcutsInCart(Purchase purchase);
}