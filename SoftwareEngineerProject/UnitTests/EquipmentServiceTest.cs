using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;

namespace SoftwareEngineerProject.UnitTests
{
    public class EquipmentServiceTest
    {
        
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

        private EquipmentTable equip = new EquipmentTable
        {
            ID = 1,
            Rented = true,
            Type = "Swimming Goggles",
            Price = 9.99m,
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
        public async Task AddEquipment_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            var result = await service.AddEquipment(equip);

            Assert.NotNull(result);
            Assert.Single(await context.EquipmentTable.ToListAsync());
        }

        [Fact]
        public async Task DeleteEquipment_DeleteRecord()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            await service.AddEquipment(equip);

            var result = await service.DeleteEquipment(1);

            Assert.True(result);
            var testDelete = await context.EquipmentTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task DeleteEquipment_EquipmentNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            var result = await service.DeleteEquipment(1);

            Assert.False(result);
        }

        [Fact]
        public async Task EditEquipment_EditRecord()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            await service.AddEquipment(equip);

            var updatedEquip = equip;
            updatedEquip.Type = "Arm Bands";

            var result = await service.EditEquipment(1, updatedEquip);

            Assert.NotNull(result);
            Assert.Equal("Arm Bands", result.Type);
        }

        [Fact]
        public async Task EditEquipment_EquipmentNotFound_ThrowError()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            var result = await Assert.ThrowsAsync<Exception>(() => service.EditEquipment(1, equip));

            Assert.Equal("Equipment not found.", result.Message);
        }

        [Fact]
        public async Task GetAllEquipment_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            context.EquipmentTable.Add(equip);
            context.SaveChanges();

            var result = await service.GetAllEquipment();

            Assert.Equal(1, result.Count);
            Assert.Equal("Swimming Goggles", result.ElementAt(0).Type);
        }

        [Fact]
        public async Task GetEquipmentById_ValidId_ReturnEquipment()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            context.EquipmentTable.Add(equip);
            context.SaveChanges();

            var result = await service.GetEquipmentById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEquipmentById_InvalidId_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            var result = await service.GetEquipmentById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task AssignOwner_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            context.EquipmentTable.Add(equip);
            context.SaveChanges();

            var result = await service.AssignOwner(1, customerMember);

            Assert.NotNull(result);
            Assert.Equal(customerMember, result.Owner);
        }

        [Fact]
        public async Task AssignOwner_EquipmentNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            context.EquipmentTable.Add(equip);
            context.SaveChanges();

            var result = await service.AssignOwner(99, customerMember);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEquipmentByCustomer_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            context.EquipmentTable.Add(equip);
            context.SaveChanges();

            var result = await service.GetEquipmentByCustomer(customerMember.ID);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AssignRentalTime_ReturnTrue()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            context.EquipmentTable.Add(equip);
            context.SaveChanges();

            var result = await service.AssignRentalTime(1, DateTime.Now);

            Assert.True(result);
        }

        [Fact]
        public async Task AssignRentalTime_EquipmentNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new EquipmentService(context);

            var result = await service.AssignRentalTime(1, DateTime.Now);

            Assert.False(result);
        }
    }
}
