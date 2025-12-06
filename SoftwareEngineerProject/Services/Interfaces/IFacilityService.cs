using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface IFacilityService
    {
        // CRUD Operations
        Task<List<FacilityTable>> GetAllFacilities();
        Task<FacilityTable> GetFacilityById(int id);
        Task<FacilityTable> AddFacility(FacilityTable fT);
        Task<FacilityTable> EditFacility(int id, FacilityTable fT);
        Task<bool> DeleteFacility(int id);

        // Facility Methods
        Task<TimeSpan> OpenDuration(int id);

        Task<FacilityDataBundle> GetFacilityBundle(int facilityId, int customerId);
    }
}
