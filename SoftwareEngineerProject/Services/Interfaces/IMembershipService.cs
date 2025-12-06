using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface IMembershipService
    {
        // CRUD Operations
        Task<List<MembershipTable>> GetAllMemberships();
        Task<MembershipTable> GetMembershipById(int id);
        Task<MembershipTable> AddMembership(MembershipTable mT);
        Task<MembershipTable> EditMembership(int id, MembershipTable mT);
        Task<bool> DeleteMembership(int id);
    }
}
