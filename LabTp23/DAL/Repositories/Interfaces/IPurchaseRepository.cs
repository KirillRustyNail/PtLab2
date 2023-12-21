using LabTp23.Models;

namespace LabTp23.DAL.Repositories.Interfaces;

public interface IPurchaseRepository
{
    public Task AddAsync(Purchase purchase);
}