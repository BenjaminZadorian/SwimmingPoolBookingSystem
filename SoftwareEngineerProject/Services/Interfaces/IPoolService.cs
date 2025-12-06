using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface IPoolService
    {
        // CRUD Operations
        Task<List<PoolTable>> GetAllPools();
        Task<PoolTable> GetPoolById(int id);
        Task<PoolTable> AddPool(PoolTable pT);
        Task<PoolTable> EditPool(int id, PoolTable pT);
        Task<bool> DeletePool(int id);
    }
}
