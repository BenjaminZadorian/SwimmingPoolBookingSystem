using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class OrderService : IOrderService
    {
        public readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<OrderTable> AddOrder(OrderTable oT)
        {
            _context.OrderTable.Add(oT);
            await _context.SaveChangesAsync();
            return oT;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var dbOrder = await _context.OrderTable.FindAsync(id);
            if (dbOrder != null)
            {
                _context.Remove(dbOrder);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<OrderTable> EditOrder(int id, OrderTable oT)
        {
            var dbOrder = await _context.OrderTable.FindAsync(id);
            if (dbOrder != null)
            {
                dbOrder.Customer = oT.Customer;
                dbOrder.OrderDate = oT.OrderDate;
                dbOrder.OrderPrice = oT.OrderPrice;
                await _context.SaveChangesAsync();
                return dbOrder;
            }
            return null;
        }

        public async Task<List<OrderTable>> GetAllOrders()
        {
            var orders = await _context.OrderTable.ToListAsync();
            return orders;
        }

        public async Task<OrderTable> GetOrderById(int id)
        {
            return await _context.OrderTable.FindAsync(id);
        }

        public async Task<OrderTable> GetLatestOrder()
        {
            var latestOrder = await _context.OrderTable.OrderByDescending(m => m.OrderDate).FirstOrDefaultAsync();
            if (latestOrder != null)
            {
                return latestOrder;
            }
            return null;
        }

        public async Task<List<OrderTable>> GetOrdersByCustomer(CustomerTable customer)
        {
            var dbOrders = await _context.OrderTable.Where(o => o.Customer == customer).ToListAsync();
            return dbOrders;
        }

        public async Task<List<OrderTable>> GetOrdersByFacility(int facilityID)
        {
            var dbOrders = await _context.OrderTable.Include(o => o.Customer).Where(o => o.FacilityId == facilityID).ToListAsync();
            return dbOrders;
        }
    }
}