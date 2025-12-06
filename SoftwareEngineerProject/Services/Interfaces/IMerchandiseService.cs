using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface IMerchandiseService
    {
        // CRUD Operations
        Task<List<MerchandiseTable>> GetAllMerchandise();
        Task<MerchandiseTable> GetMerchandiseById(int id);
        Task<MerchandiseTable> AddMerchandise(MerchandiseTable mT);
        Task<MerchandiseTable> EditMerchandise(int id, MerchandiseTable mT);
        Task<bool> DeleteMerchandise(int id);
        // Merchandise Methods
        Task<List<MerchandiseTable>> GetMerchandiseByFacility(int facilityId);
    }
}
