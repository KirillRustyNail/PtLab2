using LabTp23.DAL.Repositories.Implementations;
using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Models;
using Microsoft.EntityFrameworkCore;

namespace LabTp23Tests;

public class CartRepositoryTests : TestCommandBase
    {
        private readonly ICartRepository _cartRepository;

        public CartRepositoryTests()
        {
            _cartRepository = new CartRepository(Context);
        }

        [Fact]
        public async Task AddAsync_AddsProductToCart()
        {
            // Arrange
            var product = new Product { Name = "TestProduct", Price = 30 };
            Context.Product.Add(product);
            Context.SaveChanges();

            // Act
            await _cartRepository.AddAsync(product.ID);

            // Assert
            var cart = await Context.Cart.Include(c => c.Products).FirstAsync();
            Assert.NotNull(cart);
            Assert.Single(cart.Products);
            Assert.Equal(product.ID, cart.Products.First().ID);
        }

        [Fact]
        public async Task Clear_ClearsProductsInCart()
        {
            // Arrange
            var product1 = new Product { Name = "TestProduct1", Price = 30 };
            var product2 = new Product { Name = "TestProduct2", Price = 40 };
            Context.Product.AddRange(product1, product2);
            Context.SaveChanges();

            await _cartRepository.AddAsync(product1.ID);
            await _cartRepository.AddAsync(product2.ID);

            // Act
            await _cartRepository.Clear();

            // Assert
            var cart = await Context.Cart.Include(c => c.Products).FirstAsync();
            Assert.NotNull(cart);
            Assert.Empty(cart.Products);
        }

        [Fact]
        public async Task GetAsync_ReturnsCartWithProducts()
        {
            // Arrange
            var product1 = new Product { Name = "TestProduct1", Price = 30 };
            var product2 = new Product { Name = "TestProduct2", Price = 40 };
            Context.Product.AddRange(product1, product2);
            Context.SaveChanges();

            await _cartRepository.AddAsync(product1.ID);
            await _cartRepository.AddAsync(product2.ID);

            // Act
            var cart = await _cartRepository.GetAsync();

            // Assert
            Assert.NotNull(cart);
            Assert.Equal(2, cart.Products.Count);
        }
    }