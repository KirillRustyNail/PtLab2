using LabTp23.Models;

namespace LabTp23.DAL.Repositories.Interfaces;

public interface ICartRepository
{
    public Task<Cart> GetAsync();
    public Task AddAsync(Guid productId);
    public Task Clear();
}