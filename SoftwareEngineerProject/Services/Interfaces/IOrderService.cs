using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface IOrderService
    {
        // CRUD Operations
        Task<List<OrderTable>> GetAllOrders();
        Task<OrderTable> GetOrderById(int id);
        Task<OrderTable> AddOrder(OrderTable oT);
        Task<OrderTable> EditOrder(int id, OrderTable oT);
        Task<bool> DeleteOrder(int id);

        // Order Methods
        Task<OrderTable> GetLatestOrder();
        Task<List<OrderTable>> GetOrdersByCustomer(CustomerTable customer);
        Task<List<OrderTable>> GetOrdersByFacility(int facilityID);
    }
}
