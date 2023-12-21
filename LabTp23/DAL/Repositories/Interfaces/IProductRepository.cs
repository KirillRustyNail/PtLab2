using LabTp23.Models;

namespace LabTp23.DAL.Repositories.Interfaces;

public interface IProductRepository
{
    public Task<IList<Product>> GetProductsAsync();    
}