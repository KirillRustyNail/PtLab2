using LabTp23.DAL;
using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Models;
using LabTp23.Services.Interfaces;
using System;

namespace LabTp23.Services.Implementations;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public CartService(
        ICartRepository cartRepository,
        IPurchaseRepository purchaseRepository)
    {
        _cartRepository = cartRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<bool> AddProdcutToCart(Guid productId)
    {
        var cart = await _cartRepository.GetAsync();
        if (cart.Products.Any(x => x.ID == productId))
        {
            return false;
        }
        else
        {
            await _cartRepository.AddAsync(productId);
            return true;
        }
    }

    public async Task BuyProdcutsInCart(Purchase purchase)
    {
        
        var cart = await _cartRepository.GetAsync();
        if (string.IsNullOrWhiteSpace(purchase.Address) || string.IsNullOrWhiteSpace(purchase.Person))
        {
            //return 0;
        }
        if (cart.Products != null)
        {
            foreach (var p in cart.Products)
            {
                purchase.ProductID = p.ID;
                purchase.ID = Guid.NewGuid();
                await _purchaseRepository.AddAsync(purchase);
            }
        }
        await _cartRepository.Clear();
    }
}