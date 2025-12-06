using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;

namespace SoftwareEngineerProject.UnitTests
{
    public class LockerServiceTest
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

        private LockerTable locker = new LockerTable
        {
            ID = 1,
            Rented = false,
            Facility = facility,
            OwnerID = 1,
            Owner = customerMember,
            StartRent = DateTime.UtcNow,
            EndRent = DateTime.UtcNow.AddHours(1)
        };

        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task AddLocker_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            var result = await service.AddLocker(locker);

            Assert.NotNull(result);
            Assert.Single(await context.LockerTable.ToListAsync());
        }

        [Fact]
        public async Task DeleteLocker_RemoveRecord()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.DeleteLocker(1);

            Assert.True(result);
            var testDelete = await context.LockerTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task DeleteLocker_RecordNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.DeleteLocker(99);

            Assert.False(result);
        }

        [Fact]
        public async Task EditLocker_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var newLocker = locker;

            newLocker.Rented = true;

            var result = await service.EditLocker(1, newLocker);

            Assert.NotNull(result);
            Assert.Equal(true, result.Rented);
        }

        [Fact]
        public async Task EditLocker_LockerNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            var newLocker = locker;

            newLocker.Rented = true;

            var result = await service.EditLocker(1, newLocker);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllLockers()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.GetAllLockers();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetLockerById_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.GetLockerById(1);

            Assert.NotNull(result);
            Assert.Equal(false, result.Rented);
        }

        [Fact]
        public async Task GetLockerById_LockerNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            var result = await service.GetLockerById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetLockersByFacility_ReturnList()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.GetLockersByFacility(1);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task GetLockersByFacility_NoLockers_ReturnEmptyList()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            var result = await service.GetLockersByFacility(1);

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task GetLockersByCustomer_ReturnList()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.GetLockersByCustomer(1);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task AssignOwner_ReturnTrue()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.AssignOwner(1, customerMember);

            Assert.True(result);
        }

        [Fact]
        public async Task AssignOwner_LockerNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            var result = await service.AssignOwner(1, customerMember);

            Assert.False(result);
        }

        [Fact]
        public async Task AssignRentalTime_ReturnTrue()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            await service.AddLocker(locker);

            var result = await service.AssignRentalTime(1, DateTime.Now);

            Assert.True(result);
        }

        [Fact]
        public async Task AssignRentalTime_LockerNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new LockerService(context);

            var result = await service.AssignRentalTime(1, DateTime.Now);

            Assert.False(result);
        }
    }
}
