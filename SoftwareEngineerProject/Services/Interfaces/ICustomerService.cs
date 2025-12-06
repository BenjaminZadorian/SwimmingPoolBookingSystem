using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface ICustomerService
    {
        // CRUD Operations
        Task<List<CustomerTable>> GetAllCustomers();
        Task<CustomerTable> GetCustomerById(int id);
        Task<CustomerTable> GetCustomerByEmail(string email);
        Task<bool> AddCustomer(CustomerTable cT);
        Task<CustomerTable> EditCustomer(int id, CustomerTable cT);
        Task<bool> DeleteCustomer(int id);

        // Customer Methods
        Task<CustomerTable> BookMembership(int id, MembershipTable newMembership, int duration);
        Task<CustomerTable> CancelMembership(int id);
        Task<CustomerTable> ExtendMembership(int id, int duration);
        Task<CustomerTable> ChangeMembership(int id, string newTier);
        Task<bool> IsMember(int id);
        Task<TimeSpan> MembershipDuration(int id);
        Task<CustomerTable?> VerifyLogin(string email, string password);
        Task AddSaltAndHashed(int id, byte[] salt, string password);
        Task<OrderTable> GetLatestOrder(int id);
        Task<bool> IncreasePurchaseCount(int id);
    }
}
