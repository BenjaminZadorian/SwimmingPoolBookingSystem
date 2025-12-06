using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;

namespace SoftwareEngineerProject.UnitTests
{
    public class OrderServiceTest
    {
        private static FacilityTable facility = new FacilityTable
        {
            ID = 1,
            Name = "Name",
            Address = "Address",
            City = "City",
            PostCode = "12345",
            OpenTime = new TimeOnly(6, 0),
            CloseTime = new TimeOnly(22, 0)
        };

        private static CustomerTable customerMember = new CustomerTable
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

        private OrderTable order = new OrderTable
        {
            RowID = 1,
            OrderType = "Towel",
            CustomerId = 1,
            Customer = customerMember,
            OrderDate = DateTime.Now,
            OrderPrice = 10.00m,
            FacilityId = 1,
            Facility = facility
        };
        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task AddOrder_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);

            var result = await service.AddOrder(order);

            Assert.NotNull(result);
            Assert.Single(await context.OrderTable.ToListAsync());
        }

        [Fact]
        public async Task DeleteOrder_ReturnTrue()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);
            await service.AddOrder(order);

            var result = await service.DeleteOrder(1);

            Assert.True(result);
            var testDelete = await context.OrderTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task DeleteOrder_OrderNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);

            var result = await service.DeleteOrder(1);

            Assert.False(result);
        }

        [Fact]
        public async Task EditOrder_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);
            await service.AddOrder(order);

            var updatedOrder = order;

            updatedOrder.OrderPrice = 15.00m;

            var result = await service.EditOrder(1, updatedOrder);

            Assert.NotNull(result);
            Assert.Equal(15.00m, result.OrderPrice);
        }

        [Fact]
        public async Task EditOrder_OrderNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);

            var updatedOrder = order;

            updatedOrder.OrderPrice = 15.00m;

            var result = await service.EditOrder(1, updatedOrder);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllOrders_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);
            await service.AddOrder(order);

            var result = await service.GetAllOrders();
            Assert.Equal(1, result.Count);
            Assert.Equal("Towel", result.ElementAt(0).OrderType);
        }

        [Fact]
        public async Task GetOrderById_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);
            await service.AddOrder(order);

            var result = await service.GetOrderById(1);

            Assert.NotNull(result);
            Assert.Equal("Towel", result.OrderType);
        }

        [Fact]
        public async Task GetOrderById_OrderNotFound_ReturNull()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);

            var result = await service.GetOrderById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetLatestOrder_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);
            await service.AddOrder(order);

            var result = await service.GetLatestOrder();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetLatestOrder_NoOrders_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new OrderService(context);

            var result = await service.GetLatestOrder();

            Assert.Null(result);
        }
    }
}
