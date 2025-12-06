using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface IEquipmentService
    {
        // CRUD Operations
        Task<List<EquipmentTable>> GetAllEquipment();
        Task<EquipmentTable> GetEquipmentById(int id);
        Task<EquipmentTable> AddEquipment(EquipmentTable eT);
        Task<EquipmentTable> EditEquipment(int id, EquipmentTable eT);
        Task<bool> DeleteEquipment(int id);
        // Equipment Methods
        Task<EquipmentTable> AssignOwner(int id, CustomerTable customer);
        Task<List<EquipmentTable>> GetEquipmentByCustomer(int customerId);
        Task<List<EquipmentTable>> GetEquipmentByFacility(int facilityId);
        Task<bool> AssignRentalTime(int id, DateTime startRent);
        Task<List<EquipmentTable>> GetEquipmentOwners(int facilityId);
    }
}
