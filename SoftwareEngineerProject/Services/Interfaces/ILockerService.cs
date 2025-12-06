using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface ILockerService
    {
        // CRUD Operations
        Task<List<LockerTable>> GetAllLockers();
        Task<LockerTable> GetLockerById(int id);
        Task<LockerTable> AddLocker(LockerTable lT);
        Task<LockerTable> EditLocker(int id, LockerTable lT);
        Task<bool> DeleteLocker(int id);

        // Locker Methods
        Task<List<LockerTable>> GetLockersByFacility(int facilityId);
        Task<List<LockerTable>> GetLockersByCustomer(int customerId);
        Task<bool> AssignOwner(int id, CustomerTable cT);
        Task<bool> AssignRentalTime(int id, DateTime startRent);
        Task<List<LockerTable>> GetLockerOwners(int facilityId);
    }
}
