using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareEngineerProject.UnitTests
{
    public class CustomerServiceTest
    {

        private CustomerTable customerMember = new CustomerTable
        {
            ID = 1,
            FirstName = "Alice",
            LastName = "Johnson",
            Email = "alice.johnson@example.com",
            Password = "hashedPassword123", // This should be hashed if testing login
            Salt = new byte[] { 0x10, 0x20, 0x30, 0x40 },
            BirthDate = new DateTime(1990, 5, 20),
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddMonths(3),
            MembershipID = 1,
            Membership = new MembershipTable
            {
                ID = 1,
                Price = 79.99m,
                ShowerAccess = true,
                Tier = "Gold",
                PoolAccessStartTime = new TimeOnly(6, 0),
                PoolAccessEndTime = new TimeOnly(22, 0),
                Customer = null
            },
            AttendanceCount = 5,
            PurchasesMade = 2
        };

        private CustomerTable customerNonMember = new CustomerTable
        {
            ID = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "hashed456",
            Salt = new byte[] { 0x0A, 0x0B, 0x0C },
            BirthDate = new DateTime(1995, 6, 24),
            StartDate = new DateTime(DateTime.MinValue.Ticks).AddDays(1),
            EndDate = new DateTime(DateTime.MinValue.Ticks),
            MembershipID = null,
            Membership = null,
            AttendanceCount = 0,
            PurchasesMade = 0
        };

        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        // test adding a customer record
        [Fact]
        public async Task AddCustomer_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var result = await service.AddCustomer(customerMember);

            Assert.True(result);
            Assert.Single(await context.CustomerTable.ToListAsync());
        }

        // test adding customer record that is invalid
        [Fact]
        public async Task AddCustomer_CopyEmail_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            await service.AddCustomer(customerMember);

            var result = await service.AddCustomer(customerMember);

            Assert.False(result);
        }

        // test deleting a customer
        [Fact]
        public async Task DeleteCustomer_DeleteRecord()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            await service.AddCustomer(customerMember);

            var result = await service.DeleteCustomer(1);

            Assert.True(result);

            var testDelete = await context.CustomerTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        // test for when a customer is not in database
        [Fact]
        public async Task DeleteCustomer_CustomerNotFound_InvalidDelete()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var result = await service.DeleteCustomer(1);

            Assert.False(result);
        }

        [Fact]
        public async Task EditCustomer_EditRecord()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            await service.AddCustomer(customerMember);

            var updatedCustomer = new CustomerTable
            {
                FirstName = "Jonathan",
                LastName = "Doe",
                Email = "jon.doe@newmail.com",
                Password = "newPassword",
                Salt = new byte[] { 0x09, 0x0A },
                BirthDate = new DateTime(1990, 1, 1),
                StartDate = DateTime.UtcNow.AddDays(-5),
                EndDate = DateTime.UtcNow.AddMonths(2),
                PurchasesMade = 5
            };

            var result = await service.EditCustomer(1, updatedCustomer);

            Assert.NotNull(result);
            Assert.Equal("Jonathan", result.FirstName);
            Assert.Equal("jon.doe@newmail.com", result.Email);
            Assert.Equal("newPassword", result.Password);
            Assert.Equal(5, result.PurchasesMade);
        }

        [Fact]
        public async Task EditCustomer_CustomerNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var updatedCustomer = new CustomerTable
            {
                FirstName = "Jonathan",
                LastName = "Doe",
                Email = "jon.doe@newmail.com",
                Password = "newPassword",
                Salt = new byte[] { 0x09, 0x0A },
                BirthDate = new DateTime(1990, 1, 1),
                StartDate = DateTime.UtcNow.AddDays(-5),
                EndDate = DateTime.UtcNow.AddMonths(2),
                PurchasesMade = 5
            };

            var result = await service.EditCustomer(1, updatedCustomer);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCustomers_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.AddRange(customerMember, customerNonMember);
            context.SaveChanges();

            var result = await service.GetAllCustomers();

            Assert.Equal(2, result.Count);
            Assert.Equal("Alice", result.ElementAt(0).FirstName);
            Assert.Equal("Jane", result.ElementAt(1).FirstName);
        }

        [Fact]
        public async Task GetCustomerById_ReturnCustomer()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerMember);
            context.SaveChanges();

            var result = await service.GetCustomerById(1);

            Assert.NotNull(result);
            Assert.Equal("Alice", result.FirstName);
        }

        [Fact]
        public async Task GetCustomerById_CustomerNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var result = await service.GetCustomerById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCustomerByEmail_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerMember);
            context.SaveChanges();

            var result = await service.GetCustomerByEmail("alice.johnson@example.com");

            Assert.NotNull(result);
            Assert.Equal("Alice", result.FirstName);
        }

        [Fact]
        public async Task GetCustomerByEmail_CustomerNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var result = await service.GetCustomerByEmail("alice.johnson@example.com");

            Assert.Null(result);
        }

        [Fact]
        public async Task BookMembership_AssignMembership()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerNonMember);
            await context.SaveChangesAsync();

            var membership = new MembershipTable
            {
                ID = 1,
                Tier = "Gold",
                Price = 59.99m,
                ShowerAccess = true,
                PoolAccessStartTime = new TimeOnly(8, 0),
                PoolAccessEndTime = new TimeOnly(20, 0)
            };

            context.MembershipTable.Add(membership);

            await context.SaveChangesAsync();

            var result = await service.BookMembership(customerNonMember.ID, membership, 3);

            Assert.NotNull(result);
            Assert.Equal("Gold", result.Membership.Tier);
            Assert.Equal(DateTime.UtcNow.Date, result.StartDate);
            Assert.Equal(DateTime.UtcNow.Date.AddMonths(3), result.EndDate);
        }

        [Fact]
        public async Task BookMembership_CustomerNotFound_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var membership = new MembershipTable
            {
                ID = 1,
                Tier = "Gold",
                Price = 59.99m,
                ShowerAccess = true,
                PoolAccessStartTime = new TimeOnly(8, 0),
                PoolAccessEndTime = new TimeOnly(20, 0)
            };

            var result = await Assert.ThrowsAsync<Exception>(() => service.BookMembership(2, membership, 6));

            Assert.Equal("Customer not found.", result.Message);
        }

        [Fact]
        public async Task CancelMembership_RemoveMembershipField()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerNonMember);
            await context.SaveChangesAsync();

            var membership = new MembershipTable
            {
                ID = 1,
                Tier = "Gold",
                Price = 59.99m,
                ShowerAccess = true,
                PoolAccessStartTime = new TimeOnly(8, 0),
                PoolAccessEndTime = new TimeOnly(20, 0)
            };

            context.MembershipTable.Add(membership);

            await context.SaveChangesAsync();

            var result = await service.CancelMembership(2);

            Assert.NotNull(result);
            Assert.Null(result.Membership);
            Assert.Equal(DateTime.MinValue.AddDays(1), result.StartDate);
            Assert.Equal(DateTime.MinValue, result.EndDate);
        }

        [Fact]
        public async Task CancelMembership_CustomerNotFound_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerNonMember);
            await context.SaveChangesAsync();

            var membership = new MembershipTable
            {
                ID = 1,
                Tier = "Gold",
                Price = 59.99m,
                ShowerAccess = true,
                PoolAccessStartTime = new TimeOnly(8, 0),
                PoolAccessEndTime = new TimeOnly(20, 0)
            };

            context.MembershipTable.Add(membership);

            await context.SaveChangesAsync();

            var result = await Assert.ThrowsAsync<Exception>(() => service.CancelMembership(99));

            Assert.Equal("Customer not found.", result.Message);
        }

        [Fact]
        public async Task ChangeMembership_UpdateRecord()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerMember);
            await context.SaveChangesAsync();

            var result = await service.ChangeMembership(1, "Silver");

            Assert.NotNull(result);
            Assert.Equal("Silver", result.Membership.Tier);
        }

        [Fact]
        public async Task ChangeMembership_NoMembership_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerNonMember);
            await context.SaveChangesAsync();

            var result = await Assert.ThrowsAsync<Exception>(() => service.ChangeMembership(2, "Silver"));

            Assert.Equal("Customer does not have a valid membership.", result.Message);
        }

        [Fact]
        public async Task ChangeMembership_NoCustomer_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var result = await Assert.ThrowsAsync<Exception>(() => service.ChangeMembership(2, "Silver"));

            Assert.Equal("Customer not found.", result.Message);
        }

        [Fact]
        public async Task ExtendMembership_ChangeDuration()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerMember);
            await context.SaveChangesAsync();

            var duration = 3;

            var result = await service.ExtendMembership(1, duration);

            Assert.NotNull(result);
            Assert.Equal(DateTime.UtcNow.Date.AddMonths(3 + duration), result.EndDate);
        }

        [Fact]
        public async Task ExtendMembership_CustomerNotFound_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerMember);
            await context.SaveChangesAsync();

            var duration = 3;

            var result = await Assert.ThrowsAsync<Exception>(() => service.ExtendMembership(2, duration));

            Assert.Equal("Customer not found.", result.Message);
        }

        [Fact]
        public async Task IsMember_ReturnTrue()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerMember);
            await context.SaveChangesAsync();

            var result = await service.IsMember(1);

            Assert.True(result);
        }
        [Fact]
        public async Task IsMember_NonMember_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerNonMember);
            await context.SaveChangesAsync();

            var result = await service.IsMember(2);

            Assert.False(result);
        }

        [Fact]
        public async Task IsMember_CustomerNotFound_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerNonMember);
            await context.SaveChangesAsync();

            var result = await Assert.ThrowsAsync<Exception>(() => service.IsMember(99));

            Assert.Equal("Customer not found.", result.Message);
        }

        [Fact]
        public async Task MembershipDuration_ReturnTimeSpan()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerMember);
            await context.SaveChangesAsync();

            var result = await service.MembershipDuration(1);

            Assert.Equal((DateTime.UtcNow.Date.AddMonths(3) - DateTime.UtcNow.Date), result);
        }

        [Fact]
        public async Task MembershipDuration_NoMembership_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            context.CustomerTable.Add(customerNonMember);
            await context.SaveChangesAsync();

            var result = await Assert.ThrowsAsync<Exception>(() => service.MembershipDuration(2));

            Assert.Equal("Customer does not have a valid membership", result.Message);
        }

        [Fact]
        public async Task MembershipDuration_CustomerNotFound_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new CustomerService(context);

            var result = await Assert.ThrowsAsync<Exception>(() => service.MembershipDuration(2));

            Assert.Equal("Customer not found.", result.Message);
        }
    }
}
