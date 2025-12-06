using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class CustomerService : ICustomerService
    {

        public readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCustomer(CustomerTable cT)
        {
            List<CustomerTable> dbAllCustomers = await GetAllCustomers();
            bool UserExists = dbAllCustomers.Any(customer => customer.Email == cT.Email);

            if (UserExists != true)
            {
                _context.CustomerTable.Add(cT);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var dbCustomer = await _context.CustomerTable.FindAsync(id);
            if (dbCustomer != null)
            {
                _context.Remove(dbCustomer);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CustomerTable> EditCustomer(int id, CustomerTable cT)
        {
            var dbCustomer = await _context.CustomerTable.FindAsync(id);
            if (dbCustomer != null)
            {
                dbCustomer.FirstName = cT.FirstName;
                dbCustomer.LastName = cT.LastName;
                dbCustomer.Email = cT.Email;
                dbCustomer.Password = cT.Password;
                dbCustomer.Salt = cT.Salt;
                dbCustomer.BirthDate = cT.BirthDate;
                dbCustomer.StartDate = cT.StartDate;
                dbCustomer.EndDate = cT.EndDate;
                dbCustomer.PurchasesMade = cT.PurchasesMade;
                await _context.SaveChangesAsync();
                return dbCustomer;
            }
            return null;
        }

        public async Task<List<CustomerTable>> GetAllCustomers()
        {
            var customers = await _context.CustomerTable.ToListAsync();
            return customers;
        }

        public async Task<CustomerTable> GetCustomerById(int id)
        {
            return await _context.CustomerTable.FindAsync(id);
        }

        public async Task<CustomerTable> GetCustomerByEmail(string email)
        {
            var dbCustomer = _context.CustomerTable.Where(u => u.Email == email).FirstOrDefault();
            return dbCustomer;
        }

        // Customer Operations
        public async Task<CustomerTable> BookMembership(int id, MembershipTable newMembership, int duration)
        {
            var dbCustomer = await _context.CustomerTable.FindAsync(id);
            if (dbCustomer != null)
            {
                dbCustomer.Membership = newMembership;
                dbCustomer.StartDate = DateTime.UtcNow.Date;
                dbCustomer.EndDate = dbCustomer.StartDate.AddMonths(duration);
                await _context.SaveChangesAsync();
                return dbCustomer;
            }
            throw new Exception("Customer not found.");
        }

        public async Task<CustomerTable> CancelMembership(int id)
        {
            var dbCustomer = await _context.CustomerTable.Include(c => c.Membership).FirstOrDefaultAsync(c => c.ID == id);
            if (dbCustomer != null)
            {
                // Make membership null
                dbCustomer.Membership = null;
                dbCustomer.MembershipID = null;

                // must set date values to this as nullable DateTimes don't have the methods we need
                dbCustomer.StartDate = new DateTime(DateTime.MinValue.Ticks).AddDays(1);
                dbCustomer.EndDate = new DateTime(DateTime.MinValue.Ticks);

                await _context.SaveChangesAsync();
                return dbCustomer;
            }
            throw new Exception("Customer not found.");
        }

        public async Task<CustomerTable> ChangeMembership(int id, string newTier)
        {
            var dbCustomer = await _context.CustomerTable.Include(c => c.Membership).FirstOrDefaultAsync(c => c.ID == id);
            if (dbCustomer != null)
            { 
                if (await IsMember(id) == true) 
                {
                    dbCustomer.Membership.Tier = newTier;
                    await _context.SaveChangesAsync();
                    return dbCustomer;
                }
                throw new Exception("Customer does not have a valid membership.");
            }
            throw new Exception("Customer not found.");
        }

        // can only extend for either 1, 3, 6 or 12 months
        public async Task<CustomerTable> ExtendMembership(int id, int duration)
        {
            var dbCustomer = await _context.CustomerTable.Include(c => c.Membership).FirstOrDefaultAsync(c => c.ID == id);
            if (dbCustomer != null)
            {
                dbCustomer.EndDate = dbCustomer.EndDate.AddMonths(duration);
                await _context.SaveChangesAsync();

                return dbCustomer;
            }
            throw new Exception("Customer not found.");
        }

        public async Task<bool> IsMember(int id)
        {
            var dbCustomer = await _context.CustomerTable.Include(c => c.Membership).FirstOrDefaultAsync(c => c.ID == id);
            if (dbCustomer != null)
            {
                // && await MembershipDuration(id) > TimeSpan.Zero
                //Console.WriteLine(dbCustomer.Membership.ID);
                if (dbCustomer?.Membership != null)
                {
                    return true;
                }
                return false;   
            }
            throw new Exception("Customer not found.");
        }

        public async Task<TimeSpan> MembershipDuration(int id)
        {
            var dbCustomer = await _context.CustomerTable.Include(c => c.Membership).FirstOrDefaultAsync(c => c.ID == id);
            if (dbCustomer != null)
            {
                if (dbCustomer.Membership != null)
                {
                    return dbCustomer.EndDate - dbCustomer.StartDate;
                }
                throw new Exception("Customer does not have a valid membership");
            }
            throw new Exception("Customer not found.");
        }

        public async Task<CustomerTable?> VerifyLogin(string email, string password)
        {
            var dbAllCustomers = await GetAllCustomers();
            var chosenCustomer = dbAllCustomers.FirstOrDefault(customer => customer.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            if (chosenCustomer != null && HashingService.ValidatePassword(chosenCustomer.Salt, password, chosenCustomer.Password) == true)
            {
                return chosenCustomer;
            }
            return null;
        }

        public async Task AddSaltAndHashed(int id, byte[] salt, string password)
        {
            var dbCustomer = await _context.CustomerTable.FindAsync(id);
            if (dbCustomer != null)
            {
                dbCustomer.Salt = salt;
                dbCustomer.Password = password;
            }
            throw new Exception("Customer not found");
        }

        public async Task<OrderTable> GetLatestOrder(int id)
        {
            var dbCustomer = await GetCustomerById(id);
            if (dbCustomer != null)
            {
                // Sort all orders by specific customer by descending and return the first one
                var latestOrder = await _context.OrderTable
                    .Where(o => o.Customer == dbCustomer)
                    .OrderByDescending(o => o.OrderDate)
                    .FirstOrDefaultAsync();
                return latestOrder;
            }
            return null;

        }
        public async Task<bool> IncreasePurchaseCount(int id)
        {
            var dbCustomer = await _context.CustomerTable.FindAsync(id);
            if (dbCustomer != null)
            {
                dbCustomer.PurchasesMade++;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
